namespace PEMS.Contracts
{
    public static class Constants
    {
        public static readonly string ConnectionStringKey = "DefaultConnection";

        public static class Tables
        {
            public static readonly string PEMS = "PEMS";
        }

        public static class QueryString
        {
            public static readonly string GetPEMS = $"SELECT * FROM {Tables.PEMS}";

            public static readonly string AddPEMS = $"INSERT INTO {Tables.PEMS} (TST_PGM_CDE, TST_ADM__TST_DTE, SRC_SYS_ID, TRGT_SYS_ID, FLE_NAM, DTA_TYP_NAM, FLE_SEQ_NO, FILE_TYPE_CODE, FLE_PRCSD_DTE, TOT_RCD_CNT, PPR_BSD_TSTG_MC_RCD_CNT, PPR_BSD_TSTG_CR_RCD_CNT, CBT_MC_RCD_CNT, CBT_CR_RCD_CNT, FLE_CRETN_DTM, LST_UPDT_DTM) VALUES (:TST_PGM_CDE, :TST_ADM__TST_DTE, :SRC_SYS_ID, :TRGT_SYS_ID, :FLE_NAM, :DTA_TYP_NAM, :FLE_SEQ_NO, :FILE_TYPE_CODE, :FLE_PRCSD_DTE, :TOT_RCD_CNT, :PPR_BSD_TSTG_MC_RCD_CNT, :PPR_BSD_TSTG_CR_RCD_CNT, :CBT_MC_RCD_CNT, :CBT_CR_RCD_CNT, :FLE_CRETN_DTM, :LST_UPDT_DTM)";

            public static readonly string UpdatePEMS = $"UPDATE {Tables.PEMS} SET TST_PGM_CDE = :TST_PGM_CDE, TST_ADM__TST_DTE = :TST_ADM__TST_DTE, SRC_SYS_ID = :SRC_SYS_ID, TRGT_SYS_ID = :TRGT_SYS_ID, FLE_NAM = :FLE_NAM, DTA_TYP_NAM = :DTA_TYP_NAM, FLE_SEQ_NO = :FLE_SEQ_NO, FILE_TYPE_CODE = :FILE_TYPE_CODE, FLE_PRCSD_DTE = :FLE_PRCSD_DTE, TOT_RCD_CNT = :TOT_RCD_CNT, PPR_BSD_TSTG_MC_RCD_CNT = :PPR_BSD_TSTG_MC_RCD_CNT, PPR_BSD_TSTG_CR_RCD_CNT = :PPR_BSD_TSTG_CR_RCD_CNT, CBT_MC_RCD_CNT = :CBT_MC_RCD_CNT, CBT_CR_RCD_CNT = :CBT_CR_RCD_CNT, FLE_CRETN_DTM = :FLE_CRETN_DTM, LST_UPDT_DTM = :LST_UPDT_DTM WHERE FLE_ID = :FLE_ID";

            public static readonly string DeletePEMS = $"DELETE FROM {Tables.PEMS} WHERE FLE_ID = :FLE_ID";

            public static readonly string GetMaxId = $"SELECT NVL(MAX(P.fle_id), 0) AS MAX_VAL FROM {Tables.PEMS} P";
        }
    }
}
