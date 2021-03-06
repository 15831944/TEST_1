﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SnapEx
{
    public class BaseUI
    {
        public void InitEvent(string theDialogName, Action initialize_cb, Func<string, NXOpen.BlockStyler.BlockDialog> action)
        {
            theDialogName = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, theDialogName);
            InitEvent(action(theDialogName), initialize_cb);
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        public void InitEvent(NXOpen.BlockStyler.BlockDialog theDialog, Action initialize_cb)
        {
            var theUI = NXOpen.UI.GetUI();

           
            Func<int> okHandler = () =>
            {
                int errorCode = 0;
                try
                {
                    Apply();
                }
                catch (Exception ex)
                {
                    //---- Enter your exception handling code here -----
                    errorCode = 1;
                    theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, ex.ToString());
                }
                return errorCode;
            };

            //ok
            theDialog.AddOkHandler(new NXOpen.BlockStyler.BlockDialog.Ok(okHandler));

            //apply
            theDialog.AddApplyHandler(new NXOpen.BlockStyler.BlockDialog.Apply(okHandler));

            //init
            theDialog.AddInitializeHandler(new NXOpen.BlockStyler.BlockDialog.Initialize(() =>
            {
                try
                {
                    if (initialize_cb != null) initialize_cb();
                    Init();
                }
                catch (Exception ex)
                {
                    theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, ex.ToString());
                }
               
            }));

            //update
            theDialog.AddUpdateHandler(new NXOpen.BlockStyler.BlockDialog.Update((block) =>
            {
                try 
                {
                    Update(block);
                }
                catch (Exception ex)
                {
                    //---- Enter your exception handling code here -----
                    theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, ex.ToString());
                }
                return 0;
            }));

            //show
            theDialog.AddDialogShownHandler(new NXOpen.BlockStyler.BlockDialog.DialogShown(() => {
                try
                {
                    DialogShown();
                }
                catch (Exception ex)
                {
                    //---- Enter your exception handling code here -----
                    theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, ex.ToString());
                }
            }));

#if !NET35

            //close 
            theDialog.AddCloseHandler(new NXOpen.BlockStyler.BlockDialog.Close(() => {
                try
                {
                    Close();
                }
                catch (Exception ex)
                {
                    //---- Enter your exception handling code here -----
                    theUI.NXMessageBox.Show("Block Styler", NXOpen.NXMessageBox.DialogType.Error, ex.ToString());
                }
                return 0;
            }));
#endif
        }

        public virtual void Init() 
        {
        }

        public virtual void Apply() 
        {
          
        }

        public virtual void Update(NXOpen.BlockStyler.UIBlock block) 
        {

        }

        public virtual void DialogShown() 
        {

        }

        public virtual void Close() 
        {

        }

        public virtual void ShowMessage(string msg,string title= "Block Styler")
        {
            NXOpen.UI.GetUI().NXMessageBox.Show(title, NXOpen.NXMessageBox.DialogType.Information, msg);
        }
    }
}
