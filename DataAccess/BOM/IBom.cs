﻿using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public interface IBom
    {
        void ImportCuprum(List<EACT_CUPRUM> CupRumList, string creator, string mouldInteriorID, bool isImportEman, List<EACT_CUPRUM_EXP> cuprumEXPs = null);
        List<EACT_CUPRUM> GetCuprumList(List<string> cuprumNames, string modelNo, string partNo);
    }
}