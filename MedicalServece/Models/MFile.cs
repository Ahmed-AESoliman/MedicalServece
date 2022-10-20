using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalServece.Models
{
    public class MFile //main file
    {
        [Key]
        public int F_id { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual ICollection<FileContent> filecontent { get; set; }
        public virtual ICollection<ResultFile> resultFile { get; set; }


    }
    public class FileContent //File Content   using this when seve as img
    {
        [Key]
        public int FC_id { get; set; }
        [Required]
        public string Cat_file { get; set; } //file Category
        [Required]
        public string filePath { get; set; }
        public DateTime CreateDate { get; set; }
        public int F_id { get; set; }
        public virtual MFile mFile { get; set; }
    }
    //public class TestCategory
    //{
    //    [Key]
    //    public int Cat_id { get; set; }
    //    public string Cat_name { get; set; }
    //    public virtual ICollection<TestsList> testsList { get; set; }

    //}
    //public class TestsList
    //{
    //    [Key]
    //    public int T_id { get; set; }
    //    public string T_name { get; set; }
    //    public string T_unit { get; set; }
    //    public string T_refRange { get; set; }
    //    public  int Cat_id { get; set; }
    //    public virtual TestCategory testCategory { get; set; }
    //    public virtual ICollection<ResultFile> resultFile { get; set; }

    //}
    public class ResultFile
    {
        [Key]
        public int R_id { get; set; }
        public string Result { get; set; }
        public DateTime InserDate { get; set; }
        public int TC_Code { get; set; }
        public int F_id { get; set; }
        public virtual TestsLabContent testsLabContent { get; set; }
        public virtual MFile MFile { get; set; }


    }

    public class FileForDr
    {
        [Key]
        public int FD_id { get; set; }
        public DateTime SendDate { get; set; }
        public string UserID { get; set; }
        public string Pa_URL { get; set; }
        public virtual ApplicationUser user { get; set; }


    }


}