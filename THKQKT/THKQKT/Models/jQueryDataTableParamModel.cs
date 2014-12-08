﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace THKQKT.Models
{
    public class jQueryDataTableParamModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

    }

    public class tblCuocKiemtoanParamModel : jQueryDataTableParamModel
    {
        public string Year { get; set; }

        public string Donvi { get; set; }

        public string Status { get; set; }
        public string LinhVuc { get; set; }
    }

    public class tblUserParamModel : jQueryDataTableParamModel
    {
        public string Donvi { get; set; }
    }

    public class tblChiTieuParamModel : jQueryDataTableParamModel
    {
        public string MaCuoc { get; set; }
        public string MaChiTieu { get; set; }
    }
}