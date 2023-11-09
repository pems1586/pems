namespace PEMS.Models
{
    public class PEMSystem
    {
        public int FLE_ID { get; set; }

        public string TST_PGM_CDE { get; set; }

        public DateTime TST_ADM__TST_DTE { get; set; }

        public string SRC_SYS_ID { get; set; }

        public string TRGT_SYS_ID { get; set; }

        public string FLE_NAM { get; set; }

        public string DTA_TYP_NAM { get; set; }

        public string FLE_SEQ_NO { get; set; }

        public string FILE_TYPE_CODE { get; set; }

        public DateTime FLE_PRCSD_DTE { get; set; }

        public int TOT_RCD_CNT { get; set; }

        public int PPR_BSD_TSTG_MC_RCD_CNT { get; set; }

        public int PPR_BSD_TSTG_CR_RCD_CNT { get; set; }

        public int CBT_MC_RCD_CNT { get; set; }

        public int CBT_CR_RCD_CNT { get; set; }

        public DateTime FLE_CRETN_DTM { get; set; }

        public DateTime LST_UPDT_DTM { get; set; }
    }
}