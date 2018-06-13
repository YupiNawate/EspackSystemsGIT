﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EspackDataGrid;
using EspackFormControls;
using static MicrosoftOfficeTools.MSTools;
using EspackFileStream;

namespace Nidus
{
    public partial class fDocumentControl : Form
    {
        //public EspackFileDataContainer FdcData { get; set; } = new EspackFileDataContainer();

        public fDocumentControl()
        {
            InitializeComponent();

            //CTLM definitions
            CTLM.Conn = Values.gDatos;
            CTLM.sSPAdd = "pDocumentsCabAdd";
            CTLM.DBTable = "vDocumentControl";

            //var txtFileName = (EspackTextBox)fsFileData;
            var FdcData = new EspackFileDataContainer();
            //file containers
            fsFileData.FileData = FdcData;
            fsFileData.PDFFileData = FdcPDFData;
            //Header
            CTLM.AddItem(txtDocumentCode, "DocumentCode",false,false,false,1,true,true);
            CTLM.AddItem(txtTypeCode, "TypeCode", true, false, false, 2, false, true);
            CTLM.AddItem(txtSubtype, "SubTypeCode", true, false, false, 2, false, true);
            CTLM.AddItem(txtSection, "SectionCode", true, false, false, 2, false, true);
            CTLM.AddItem(txtTitle, "Title", true, false, false, 2, false, true);
            CTLM.AddItem(fsFileData, "DocumentFileName", true, false, false, 0, false, true);
            CTLM.AddItem(FdcData, "DATA", true, false, false, 2, false, false);
            CTLM.AddItem(FdcPDFData, "PDFDATA", true, false, false, 2, false, false);
            CTLM.AddItem(txtEdition, "Edition", false, false, false, 2, false, true);
            CTLM.AddItem(txtStatus, "Status", false, false, false, 2, false, true);
            CTLM.AddItem(lstFlags, "flags", true, false, false, 0, false, true);

            //Fields
            lstFlags.Source("Select codigo,DescFlagEng from flags where Tabla='DocumentsCab'");
            fsFileData.BtnSearch.Click += BtnSearch_Click;
            VS.Conn = Values.gDatos;
            VS.SQL = "Select TypeCode,SectionCode,Title from DocumentsCab ";
            VS.Start();
            //Resize += FDocumentControl_Resize;

            VS.UpdateEspackControl();
            CTLM.AddItem(VS);
            //start
            CTLM.ReQuery = true;
            CTLM.AddDefaultStatusStrip();
            CTLM.Start();
            VS.FilterRowEnabled = true;
            this.Load += FDocumentControl_Load;

            //AcroPDFLib.AcroPDF acroPDF = new AcroPDFLib.AcroPDFClass();

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text == "")
                txtTitle.Text = Path.GetFileNameWithoutExtension(fsFileData.FileName);
        }

        private void TxtTest_ValueChanged(object sender, EspackFormControls.ValueChangedEventArgs e)
        {
            Debug.Print("caca");//throw new NotImplementedException();
        }

        private void FDocumentControl_Load(object sender, EventArgs e)
        {
            VS.AddFilterCell(EspackCellTypes.CHECKEDCOMBO, 0, "Select distinct Type=TypeCode,TypeCode from DocumentsCab");
            VS.AddFilterCell(EspackCellTypes.CHECKEDCOMBO, 1, "Select distinct Section=SectionCode,SectionCode from DocumentsCab");
            VS.AddFilterCell(EspackCellTypes.TEXT, 2, "Select distinct Title from DocumentsCab");
        }

    }
}
