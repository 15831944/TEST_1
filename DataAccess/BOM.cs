﻿using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace DataAccess
{
    public abstract class BOM
    {
        /// <summary>
        /// 获取所有的共用电极信息
        /// </summary>
        public static List<EACT_CUPRUM> GetCuprumList(List<string> cuprumNames,string modelNo,string partNo)
        {
            if (cuprumNames.Count <= 0) return new List<EACT_CUPRUM>();
            using (var conn = DAL.GetConn())
            {
                conn.Open();
                var sql = new StringBuilder();
                sql.AppendFormat("select * from EACT_CUPRUM ec where (ec.STEELMODELSN !='{0}' or ec.STEELMODULESN !='{1}') and ec.PARTFILENAME in ", modelNo,partNo);
                sql.Append("(");
                sql.Append(string.Join(",", Enumerable.Select(cuprumNames, u => string.Format("'{0}'", u)).ToArray()));
                sql.Append(")");
                var QueryList = conn.Query<EACT_CUPRUM>(sql.ToString(), null).ToList();
                return QueryList;
            }
        }

        /// <summary>
        /// 删除共用电极
        /// </summary>
        private static string DeleteCuprumEx(List<EACT_CUPRUM_EXP> cuprumNames)
        {
            var sql = new StringBuilder();
            sql.Append("delete  from EACT_CUPRUM_EXP  where ");
            sql.AppendFormat("EACT_CUPRUM_EXP.MODELNO='{0}' and ", cuprumNames.FirstOrDefault().MODELNO);
            sql.AppendFormat("EACT_CUPRUM_EXP.PARTNO='{0}' and ", cuprumNames.FirstOrDefault().PARTNO);
            sql.Append("EACT_CUPRUM_EXP.CUPRUMID in ");
            sql.Append("(select EACT_CUPRUM.CUPRUMID from EACT_CUPRUM where EACT_CUPRUM.CUPRUMNAME in ");
            sql.Append("(");
            sql.Append(string.Join(",", Enumerable.Select(cuprumNames, u => string.Format("'{0}'", u.CUPRUMNAME)).Distinct().ToArray()));
            sql.Append(")");
            sql.Append(")");
            return sql.ToString();
        }

        /// <summary>
        /// 新增共用电极
        /// </summary>
        private static string InsertCuprumEx(EACT_CUPRUM_EXP exp) 
        {
            return string.Format("insert into EACT_CUPRUM_EXP(CUPRUMID,MODELNO,PARTNO,X,Y,Z,C) output inserted.ID values(@CUPRUMID,@MODELNO,@PARTNO,@X,@Y,@Z,@C)");
        }

        /// <summary>
        /// 获取电极列表
        /// </summary>
        public static List<EACT_CUPRUM> GetCuprumList(string cuprumName) 
        {
            using (var conn = DAL.GetConn()) 
            {
                conn.Open();
                var sql = new StringBuilder();
                sql.Append("select * from EACT_CUPRUM ec ");
                sql.Append("inner join EACT_MOULD em on ec.STEELMODELSN = em.SN where ec.CUPRUMNAME like '%'+ec.STEELMODELSN+'-'+ec.STEELMODULESN+'-'+@cuprumName+'%'");
                var QueryList = conn.Query<EACT_CUPRUM>(sql.ToString(), new { cuprumName = cuprumName }).ToList();
                return QueryList;
            }
        }
        /// <summary>
        /// 导BOM
        /// </summary>
        public static void ImportCuprum(List<EACT_CUPRUM> CupRumList, string creator, string mouldInteriorID,bool isImportEman, List<EACT_CUPRUM_EXP> cuprumEXPs = null)
        {
            using (var conn = DAL.GetConn())
            {
                conn.Open();
                var _tran = conn.BeginTransaction();
                try
                {
                    if (isImportEman)
                    { //导入Eman
                        Eman.ImportEman(conn, _tran, CupRumList, mouldInteriorID, creator);
                    }
                    if (CupRumList.Count > 0)
                    {
                        string ids = string.Join(",", Enumerable.Select(CupRumList, u => string.Format("'{0}'", u.CUPRUMSN)).ToArray());
                        //TODO 先判断待导入的电极在库中是否已经存在，如果存在则先删除再插入
                        
                        //清除符合条件的预装表数据
                        var delete_cuprum_assembly_sql = string.Format("delete from EACT_cuprum_assembly where CUPRUMID in(select cuprumid from EACT_cuprum where CUPRUMSN in({0}))", ids.TrimEnd(','));
                        
                        //清除符合条件的电极表数据
                        var delete_cuprum_sql = string.Format("delete from EACT_cuprum where CUPRUMSN in({0})", ids.TrimEnd(','));
                        
                        //插入钢件并返回插入的钢件ID
                        var select_mould_sql = string.Format("select mouldid from EACT_mould where sn='{0}'", CupRumList[0].STEELMODELSN);
                        var insert_mould_sql = string.Format("insert into EACT_mould(SN,DESIGNER,DESIGNERTIME) output inserted.mouldid values('{0}','{1}',getdate())", CupRumList[0].STEELMODELSN, creator);

                        conn.Execute(delete_cuprum_assembly_sql, null, _tran);
                        conn.Execute(delete_cuprum_sql, null, _tran);
                        object mouldId = conn.ExecuteScalar(select_mould_sql, null, _tran, null, null);
                        if (mouldId == null)
                        {
                            mouldId = conn.ExecuteScalar(insert_mould_sql, null, _tran, null, null).ToString();
                        }
                        if (mouldId != null) 
                        {
                            foreach (var item in CupRumList)
                            {
                                //插入电极表并返回插入的电极ID
                                var insert_cuprum_sql = string.Empty;
                                insert_cuprum_sql = "insert into EACT_cuprum(mouldid,cuprumname,cuprumsn,frienum,vdi,struff,strufftype,STYLIST,";
                                insert_cuprum_sql += "Dateofdelivery,openstruff,discharging,Shape,rock,Procdirection,Rmf,offsetx,offsety,";
                                insert_cuprum_sql += "x,y,z,c,steel,Substratecquadrant,STEELMODELSN,STEELMODULESN,Assemblyexp,PARTFILENAME,HEADPULLUPH,STRETCHH,STRUFFGROUPL,UNIT";
                                insert_cuprum_sql += ",PROCESSNUM";
                                insert_cuprum_sql += ") output inserted.cuprumid values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}',";
                                insert_cuprum_sql += "'{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},{16},";
                                insert_cuprum_sql += "'{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}')";
                                insert_cuprum_sql = string.Format(insert_cuprum_sql,
                                    mouldId ,item.CUPRUMNAME , item.CUPRUMSN , item.FRIENUM, item.VDI, item.STRUFF, item.STRUFFTYPE
                                    , creator, item.DATEOFDELIVERY, item.OPENSTRUFF, item.DISCHARGING, item.SHAPE, item.ROCK
                                    , item.PROCDIRECTION, item.RMF, DecimalConvert(item.OFFSETX)
                                    , DecimalConvert(item.OFFSETY), item.X, item.Y, item.Z, item.C, item.STEEL, item.SUBSTRATECQUADRANT
                                     , item.STEELMODELSN, item.STEELMODULESN, item.ASSEMBLYEXP, item.PARTFILENAME, item.HEADPULLUPH, item.STRETCHH
                                     , item.STRUFFGROUPL , item.UNIT,item.PROCESSNUM
                                    );
                                string cuprumId = conn.ExecuteScalar(insert_cuprum_sql, null, _tran, null, null).ToString();

                                //共用电极
                                if (cuprumEXPs != null) 
                                {
                                    var shareElec = new DataAccess.Model.EACT_CUPRUM_EXP();
                                    shareElec.CUPRUMID = decimal.Parse(cuprumId);
                                    shareElec.PARTNO = item.STEELMODULESN;
                                    shareElec.MODELNO = item.STEELMODELSN;
                                    shareElec.X = item.X;
                                    shareElec.Y = item.Y;
                                    shareElec.Z = item.Z;
                                    shareElec.C=item.C;
                                    cuprumEXPs.Add(shareElec);
                                }

                                //插入物料表(如果匹配记录不存在则新增一条)
                                var select_cuprum_struff_sql = string.Format("select struffid from EACT_cuprum_struff where STRUFFNAME='{0}'", item.STRUFF);
                                object struffId = conn.ExecuteScalar(select_cuprum_struff_sql, null, _tran, null, null);
                                if (struffId == null)
                                {
                                    var insert_cuprum_struff_sql = string.Format("insert into EACT_cuprum_struff output inserted.struffid select '{0}' where not exists(select * from EACT_cuprum_struff where STRUFFNAME='{0}')", item.STRUFF);
                                    struffId = conn.ExecuteScalar(insert_cuprum_struff_sql, null, _tran,null,null).ToString();
                                }
                                //插入物料规格(如果匹配记录不存在则新增一条)
                                var select_cuprum_spec_sql = string.Format("select specid from EACT_cuprum_spec where specl={0} and specw={1} and spech={2}", item.EDMCONDITIONSN.ToLower().Split('x')[0], item.EDMCONDITIONSN.ToLower().Split('x')[1], item.EDMCONDITIONSN.ToLower().Split('x')[2]);
                                object specId = conn.ExecuteScalar(select_cuprum_spec_sql, null, _tran, null, null);
                                if (specId == null)
                                {
                                    var insert_cuprum_spec_sql = "insert into EACT_cuprum_spec(specnname,specexp,specl,specw,spech) output inserted.specid ";
                                    insert_cuprum_spec_sql += " select '{0}*{1}*{2}','{0}*{1}*{2}',{0},{1},{2} ";
                                    insert_cuprum_spec_sql += " where not exists(select * from EACT_cuprum_spec where specl={0} and specw={1} and spech={2})";
                                    insert_cuprum_spec_sql = string.Format(insert_cuprum_spec_sql, item.EDMCONDITIONSN.ToLower().Split('x')[0], item.EDMCONDITIONSN.ToLower().Split('x')[1], item.EDMCONDITIONSN.ToLower().Split('x')[2]);
                                    specId = conn.ExecuteScalar(insert_cuprum_spec_sql, null, _tran, null, null).ToString();
                                }
                                //匹配夹具（根据长宽的取值区间，取第一个符合条件的；如果找不到匹配项，默认取第一条记录）
                                var select_chuck_type_sql = "select top 1 chucktypeid from";
                                select_chuck_type_sql += "(select chucktypeid from EACT_chuck_type where (specmaxw>={0} and specminw<={0}) and (specmaxl>={1} and specminl<={1})";
                                select_chuck_type_sql += " union all ";
                                select_chuck_type_sql += "select min(chucktypeid) from EACT_chuck_type) t";
                                select_chuck_type_sql = string.Format(select_chuck_type_sql, item.EDMCONDITIONSN.ToLower().Split('x')[0], item.EDMCONDITIONSN.ToLower().Split('x')[1]);
                                string chuckId = conn.ExecuteScalar(select_chuck_type_sql, null, _tran, null, null).ToString();
                                //插入电极预装表
                                var insert_cuprum_assembly_sql = "insert into EACT_cuprum_assembly(cuprumid,specid,struffid,chucktypeid,RECORDTIME)";
                                insert_cuprum_assembly_sql += " values(" + cuprumId + "," + specId + "," + struffId + "," + chuckId + ",getdate())";
                                conn.Execute(insert_cuprum_assembly_sql, null, _tran);
                            }
                        }
                    }

                    //删除
                    if (cuprumEXPs != null) 
                    {
                        if (cuprumEXPs.Count > 0) { conn.Execute(DeleteCuprumEx(cuprumEXPs), null, _tran, null, null); }
                        cuprumEXPs.ForEach(u =>
                        {
                            conn.Execute(InsertCuprumEx(u), u, _tran, null, null);
                        });
                    }

                    _tran.Commit();
                }
                catch(Exception ex)
                {
                    _tran.Rollback();
                    throw ex;
                }
            }
        }

        static string DecimalConvert(decimal? d)
        {
            return d == null ? decimal.Zero.ToString() : d.ToString();
        }
    }
}