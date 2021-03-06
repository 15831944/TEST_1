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
        public static bool IsConnect()
        {
            return GetBomDal().IsConnect();
        }
        /// <summary>
        /// 上传取点记录
        /// </summary>
        public static void UploadAutoCMMRecord(EACT_AUTOCMM_RECORD record)
        {
            GetBomDal().UploadAutoCMMRecord(record);
        }
        public static void UpdateCuprumDISCHARGING(List<EACT_CUPRUM> CupRumList) { GetBomDal().UpdateCuprumDISCHARGING(CupRumList); }
        public static List<EACT_CUPRUM> GetCuprumList(List<string> cuprumNames, string modelNo, string partNo)
        {
            return GetBomDal().GetCuprumList(cuprumNames, modelNo, partNo);
        }

        public static void ImportCuprum(List<EACT_CUPRUM> CupRumList, string creator, string mouldInteriorID, bool isImportEman, string emanWebPath, List<EACT_CUPRUM_EXP> cuprumEXPs = null)
        {
            GetBomDal().ImportCuprum(CupRumList, creator, mouldInteriorID, isImportEman, emanWebPath, cuprumEXPs);
        }

        private static IBom GetBomDal()
        {
            return new BomV1();
        }
    }
}
