﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SnapEx;

namespace EactBom
{
    public partial class FrmEactBom : Form
    {
        public FrmEactBom()
        {
            InitializeComponent();
            InitUI();
            InitEvent();
            this.Shown += FrmEactBom_Shown;
        }

        public FrmEactBom(ElecManage.MouldInfo mouldInfo) 
        {
            InitializeComponent();
            InitUI();
            InitEvent();
            this.Shown += (sender,e) => {
                SplashScreen.Splasher.Show(typeof(SplashScreen.FrmSplashScreen));
                //System.Threading.Thread.Sleep(800);
                SplashScreen.Splasher.Status = "正在加载电极数据......";
                System.Threading.Thread.Sleep(800);
                try 
                {
                    dgvSteels.DataSource = new List<ElecManage.MouldInfo>() { mouldInfo };
                    SplashScreen.Splasher.Status = "加载完毕............";
                    System.Threading.Thread.Sleep(800);

                    SplashScreen.Splasher.Close();
                }
                catch (Exception ex)
                {
                    SplashScreen.Splasher.Close();
                    throw ex;
                }
            };
        }

        void InitUI() 
        {
            Snap.UI.WinForm.SetApplicationIcon(this);
            Snap.UI.WinForm.ReparentForm(this);
            Text = "EACT-益模智能加工系统";

            var configData = EactBomBusiness.Instance.ConfigData;

            if (configData.IsCanPropertyUpdate)
            {
                txtFINISH_NUMBER.ReadOnly = false;
                txtMIDDLE_NUMBER.ReadOnly = false;
                txtROUGH_NUMBER.ReadOnly = false;
                txtFINISH_SPACE.ReadOnly = false;
                txtMIDDLE_SPACE.ReadOnly = false;
                txtROUGH_SPACE.ReadOnly = false;
            }
            btnSave.Visible = configData.IsCanPropertyUpdate;
            //btnShareElec.Visible = configData.ShareElec;
            groupShareElec.Visible = configData.ShareElec;
            configData.Poperties.ForEach(u => {
                ComboBox cbb = null;
                var listCbb = new List<ComboBox>();
                if (u.DisplayName == "电极材质") 
                {
                    cbb = cboxMAT_NAME;
                    listCbb.Add(cboxM_MAT_NAME);
                    listCbb.Add(cboxR_MAT_NAME);
                }
                else if (u.DisplayName == "加工方向")
                {
                    cbb = cbbProdirection;
                }
                else if (u.DisplayName == "电极类型")
                {
                    cbb = cbbElecType;
                }
                else if (u.DisplayName == "摇摆方式")
                {
                    cbb = cbbRock;
                }
                else if (u.DisplayName == "精公光洁度")
                {
                    cbb = cbbFSmoth;
                }
                else if (u.DisplayName == "中公光洁度")
                {
                    cbb = cbbMSmoth;
                }
                else if (u.DisplayName == "粗公光洁度")
                {
                    cbb = cbbRSmoth;
                }
                else if (u.DisplayName == "夹具类型")
                {
                    cbb = cbbChuckType;
                }

                if (cbb != null)
                {
                    listCbb.Add(cbb);
                }

                foreach (var item in listCbb)
                {
                    item.Items.Add(new ComboBoxItem { Text = string.Empty, Value = string.Empty });
                    u.Selections.ForEach(m =>
                    {
                        item.Items.Add(new ComboBoxItem { Text = m.Value, Value = m.Value });
                    });
                }
            });

            InitDgv(dgvCuprums);

            dgvCuprums.ReadOnly = false;
            var ChCol = new DataGridViewCheckBoxColumn();
            ChCol.HeaderText = "选择";
            ChCol.Width = 40;
            ChCol.DataPropertyName = "Checked";
            ChCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvCuprums.Columns.Add(ChCol);
            
            var txtCol = new DataGridViewTextBoxColumn();
            txtCol.DataPropertyName = "ElectName";
            txtCol.HeaderText = "电极名称";
            txtCol.ReadOnly = true;
            txtCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvCuprums.Columns.Add(txtCol);

            var txtShareElec = new DataGridViewTextBoxColumn();
            txtShareElec.DataPropertyName = "ShareElecStr";
            txtShareElec.HeaderText = "共用";
            txtShareElec.Width = 40;
            txtShareElec.ReadOnly = true;
            txtShareElec.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            txtShareElec.Visible = configData.ShareElec;
            dgvCuprums.Columns.Add(txtShareElec);

            InitDgv(dgvSteels);
            InitDgv(dgvPositions);
            InitDgv(dataGridView1);
        }

        void InitDgv(DataGridView view)
        {
            view.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            view.ReadOnly = true;
            //view.ColumnHeadersVisible = false;
            view.RowHeadersVisible = false;
            view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.AllowUserToResizeRows = false; 
            view.MultiSelect = false;
        }

        void RefreshUI() 
        {
            groupShareElec.Visible = false;
            groupBoxElecInfo.Enabled = true;
            dataGridView1.DataSource = null;
            //当前行
            var currentRow = dgvCuprums.CurrentRow;
            if (currentRow != null && currentRow.Index >= 0)
            {
                var item = currentRow.DataBoundItem as ViewElecInfo;
                var shareElecList = new List<ShareElecInfo>();
                item.ShareElecList.ForEach(u => {
                    shareElecList.Add(new ShareElecInfo { EACT_CUPRUM = u });
                });
                dataGridView1.DataSource = shareElecList;
                groupShareElec.Visible = EactBomBusiness.Instance.ConfigData.ShareElec && item.ShareElec();
                groupBoxElecInfo.Enabled = !groupShareElec.Visible;
            }
        }

        void InitEvent() 
        {
            dgvSteels.SelectionChanged += dgvSteels_SelectionChanged;
            dgvCuprums.SelectionChanged += dgvCuprums_SelectionChanged;
            ckCuprumSelectAll.CheckedChanged += ckCuprumSelectAll_CheckedChanged;
            dgvCuprums.DataSourceChanged += dgvCuprums_DataSourceChanged;
            btnSave.Click += btnSave_Click;
            this.FormClosing += FrmEactBom_FormClosing;
        }

        private void FrmEactBom_FormClosing(object sender, FormClosingEventArgs e)
        {
            //高亮显示20181126
            if (EactBomBusiness.Instance.ConfigData.Edition == 2)
            {
                Snap.Globals.WorkPart.Bodies.ToList().ForEach(u =>
                {
                    if (u.IsHighlighted)
                    {
                        u.IsHighlighted = false;
                    }
                });
            }

        }

        void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var positions = (dgvPositions.DataSource as List<ElecManage.PositioningInfo>) ?? new List<ElecManage.PositioningInfo>();
                if (positions.Count > 0)
                {
                    positions.ForEach(u =>
                    {
                        var info = u.Electrode.GetElectrodeInfo();
                        int tempIntResult = 0;
                        if (int.TryParse(txtFINISH_NUMBER.Text.Trim(), out tempIntResult))
                        {
                            info.FINISH_NUMBER = tempIntResult;
                        }
                        else
                        {
                            throw new Exception("精公数量错误，请检查");
                        }
                        if (int.TryParse(txtMIDDLE_NUMBER.Text.Trim(), out tempIntResult))
                        {
                            info.MIDDLE_NUMBER = tempIntResult;
                        }
                        else
                        {
                            throw new Exception("中公数量错误，请检查");
                        }
                        if (int.TryParse(txtROUGH_NUMBER.Text.Trim(), out tempIntResult))
                        {
                            info.ROUGH_NUMBER = tempIntResult;
                        }
                        else
                        {
                            throw new Exception("粗公数量错误，请检查");
                        }
                        double tempRealResult = 0;
                        if (double.TryParse(txtFINISH_SPACE.Text.Trim(), out tempRealResult))
                        {
                            info.FINISH_SPACE = tempRealResult;
                        }
                        else
                        {
                            throw new Exception("精公火花位错误，请检查");
                        }
                        if (double.TryParse(txtMIDDLE_SPACE.Text.Trim(), out tempRealResult))
                        {
                            info.MIDDLE_SPACE = tempRealResult;
                        }
                        else
                        {
                            throw new Exception("中公火花位错误，请检查");
                        }
                        if (double.TryParse(txtROUGH_SPACE.Text.Trim(), out tempRealResult))
                        {
                            info.ROUGH_SPACE = tempRealResult;
                        }
                        else
                        {
                            throw new Exception("粗公火花位错误，请检查");
                        }
                        info.EDMPROCDIRECTION = cbbProdirection.Text;
                        info.EDMROCK = cbbRock.Text;
                        info.UNIT = cbbElecType.Text;
                        info.F_SMOOTH = cbbFSmoth.Text;
                        info.M_SMOOTH = cbbMSmoth.Text;
                        info.R_SMOOTH = cbbRSmoth.Text;
                        info.MAT_NAME = cboxMAT_NAME.Text;
                        info.M_MAT_NAME = cboxM_MAT_NAME.Text;
                        info.R_MAT_NAME = cboxR_MAT_NAME.Text;
                        info.ELEC_CLAMP_GENERAL_TYPE = cbbChuckType.Text;
                    });
                    MessageBox.Show("保存属性成功");   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void dgvCuprums_DataSourceChanged(object sender, EventArgs e)
        {
            var cuprums = dgvCuprums.DataSource as List<ViewElecInfo> ?? new List<ViewElecInfo>();
            labelCuprumNum.Text = string.Format("电极总数量:{0}",cuprums.Count);
        }

        void ckCuprumSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            var cuprums = dgvCuprums.DataSource as List<ViewElecInfo> ?? new List<ViewElecInfo>();
            cuprums.ForEach(u => {
                u.Checked = ckCuprumSelectAll.Checked;
            });
            dgvCuprums.Refresh();
        }

        ViewElecInfo _curCuprum;
        void dgvCuprums_SelectionChanged(object sender, EventArgs e)
        {
            //TODO 清空属性区域


            //当前行
            var currentRow = dgvCuprums.CurrentRow;
            var steelInfo = dgvSteels.CurrentRow.DataBoundItem as ElecManage.MouldInfo;
            if (steelInfo != null && currentRow != null && currentRow.Index >= 0)
            {
                var item = currentRow.DataBoundItem as ViewElecInfo;
                if (_curCuprum != item)
                {
                    var list=EactBomBusiness.Instance.GetPositioningInfos(item,steelInfo);
                    dgvPositions.DataSource =list ;
                    if (list.Count > 0)
                    {
                        var pos = list.First();
                        var info = pos.Electrode.GetElectrodeInfo();
                        txtFINISH_NUMBER.Text = info.FINISH_NUMBER.ToString();
                        txtMIDDLE_NUMBER.Text = info.MIDDLE_NUMBER.ToString();
                        txtROUGH_NUMBER.Text = info.ROUGH_NUMBER.ToString();
                        txtFINISH_SPACE.Text = info.FINISH_SPACE.ToString();
                        txtMIDDLE_SPACE.Text = info.MIDDLE_SPACE.ToString();
                        txtROUGH_SPACE.Text = info.ROUGH_SPACE.ToString();
                        cboxMAT_NAME.Text = info.MAT_NAME;
                        cboxM_MAT_NAME.Text = info.M_MAT_NAME;
                        cboxR_MAT_NAME.Text = info.R_MAT_NAME;
                        cbbElecType.Text = info.UNIT;
                        cbbFSmoth.Text = info.F_SMOOTH;
                        cbbMSmoth.Text = info.M_SMOOTH;
                        cbbRSmoth.Text = info.R_SMOOTH;
                        cbbRock.Text = info.EDMROCK;
                        cbbChuckType.Text = info.ELEC_CLAMP_GENERAL_TYPE;
                        cbbProdirection.Text = info.EDMPROCDIRECTION;
                        txtElecSize.Text = info.ElecSize;
                    }

                    //高亮显示20181126
                    if (EactBomBusiness.Instance.ConfigData.Edition == 2)
                    {
                        Snap.Globals.WorkPart.Bodies.ToList().ForEach(u => {
                            if (!u.IsHidden)
                            {
                                u.IsHidden = true;
                            }
                            if (u.IsHighlighted)
                            {
                                u.IsHighlighted = false;
                            }
                        });

                        list.ForEach(u => {
                            u.Electrode.ElecBody.IsHidden = false;
                            u.Electrode.ElecBody.IsHighlighted = true;
                        });

                        var workPart = Snap.Globals.WorkPart.NXOpenPart;
                        steelInfo.MouldBody.IsHidden = false;


                        workPart.ModelingViews.WorkView.Fit();
                        Snap.Globals.WorkPart.NXOpenPart.Views.Refresh();

                    }


                    _curCuprum = item;
                }

            }
            else
            {
                dgvPositions.DataSource = null;
                _curCuprum = null;
            }

           
            RefreshUI();
        }

        ElecManage.MouldInfo _curSteel;
        void dgvSteels_SelectionChanged(object sender, EventArgs e)
        {
            //当前行
            var currentRow = dgvSteels.CurrentRow;
            if (currentRow != null && currentRow.Index >= 0)
            {
                var item = currentRow.DataBoundItem as ElecManage.MouldInfo;
                if (_curSteel != item)
                {
                    dgvCuprums.DataSource = EactBomBusiness.Instance.GetElecList(item, (s) => {
                        SplashScreen.Splasher.Status = s;
                    });
                }
                _curSteel = item;
            }
            else 
            {
                _curSteel = null;
                dgvCuprums.DataSource = null;
            }
        }


        void FrmEactBom_Shown(object sender, EventArgs e)
        {
            var list = EactBomBusiness.Instance.GetMouldInfo();
            dgvSteels.DataSource = list;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvSteels.CurrentRow == null) return;
            var steelInfo = dgvSteels.CurrentRow.DataBoundItem as ElecManage.MouldInfo;
            var positions = dgvPositions.DataSource as List<ElecManage.PositioningInfo>;
            var cuprums = dgvCuprums.DataSource as List<ViewElecInfo> ?? new List<ViewElecInfo>();
            if (steelInfo !=null&& cuprums.Where(u=>u.Checked).Count()>0)
            {
                SplashScreen.Splasher.Show(typeof(SplashScreen.FrmSplashScreen));
                //System.Threading.Thread.Sleep(800);
                SplashScreen.Splasher.Status = "正在导入EACT......";
                System.Threading.Thread.Sleep(800);
                try
                {
                    EactBomBusiness.Instance.ExportEact(cuprums, steelInfo, (s) => {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(() => { SplashScreen.Splasher.Status = s; }));
                        }
                        else 
                        {
                            SplashScreen.Splasher.Status = s;
                        }
                    }, EactBomBusiness.Instance.ConfigData.ExportPrt, EactBomBusiness.Instance.ConfigData.ExportStp
                    , EactBomBusiness.Instance.ConfigData.ExportCNCPrt,
                    EactBomBusiness.Instance.ConfigData.IsAutoPrtTool,false, EactBomBusiness.Instance.ConfigData.IsExportEDM
                    );
                    SplashScreen.Splasher.Status = "导入完毕............";
                    System.Threading.Thread.Sleep(800);
                    SplashScreen.Splasher.Close();
                    MessageBox.Show("导入成功");
                    this.BringToFront();
                }
                catch(Exception ex)
                {
                    SplashScreen.Splasher.Close();
                    throw ex;
                }
            }
            else 
            {
                MessageBox.Show("未选择电极");
            }
        }
    }
}
