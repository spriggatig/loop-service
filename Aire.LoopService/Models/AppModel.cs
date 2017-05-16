using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aire.LoopService.Models
{
    public class AppModel
    {
        public int id { get; set; }
        public string desc { get; set; }
        public int num_il_tl { get; set; }
        public int num_bc_sats { get; set; }
        public int total_il_high_credit_limit { get; set; }
        public int acc_open_past_24mths { get; set; }
        public int delinq_2yrs { get; set; }
        public int pub_rec_bankruptcies { get; set; }
        public int mo_sin_rcnt_rev_tl_op { get; set; }
        public int num_actv_rev_tl { get; set; }
        public int open_il_6m { get; set; }
        public string verification_status { get; set; }
        public int mths_since_recent_inq { get; set; }
        public int num_tl_op_past_12m { get; set; }
        public int tax_liens { get; set; }
        public string issue_d { get; set; }
        public int mths_since_last_delinq { get; set; }
        public int total_cu_tl { get; set; }
        public int mort_acc { get; set; }
        public int inq_last_6mths { get; set; }
        public int inq_last_12m { get; set; }
        public int num_actv_bc_tl { get; set; }
        public string addr_state { get; set; }
        public int num_op_rev_tl { get; set; }
        public int open_acc_6m { get; set; }
        public int mo_sin_rcnt_tl { get; set; }
        public int num_tl_120dpd_2m { get; set; }
        public int total_rev_hi_lim { get; set; }
        public string emp_title { get; set; }
        public int delinq_amnt { get; set; }
        public int mths_since_recent_bc { get; set; }
        public int total_bal_il { get; set; }
        public double bc_util { get; set; }
        public int num_tl_30dpd { get; set; }
        public double dti { get; set; }
        public int avg_cur_bal { get; set; }
        public string purpose { get; set; }
        public string mths_since_last_major_derog { get; set; }
        public int bc_open_to_buy { get; set; }
        public int open_il_12m { get; set; }
        public int revol_bal { get; set; }
        public int tot_hi_cred_lim { get; set; }
        public int num_sats { get; set; }
        public double percent_bc_gt_75 { get; set; }
        public int tot_cur_bal { get; set; }
        public int total_bc_limit { get; set; }
        public double il_util { get; set; }
        public string home_ownership { get; set; }
        public string earliest_cr_line { get; set; }
        public int open_rv_12m { get; set; }
        public int num_bc_tl { get; set; }
        public string mths_since_recent_bc_dlq { get; set; }
        public int num_tl_90g_dpd_24m { get; set; }
        public int num_rev_accts { get; set; }
        public double pct_tl_nvr_dlq { get; set; }
        public string zip_code { get; set; }
        public string revol_util { get; set; }
        public int collection_recovery_fee { get; set; }
        public int open_acc { get; set; }
        public int open_il_24m { get; set; }
        public int mo_sin_old_il_acct { get; set; }
        public int acc_now_delinq { get; set; }
        public int inq_fi { get; set; }
        public string mths_since_last_record { get; set; }
        public string emp_length { get; set; }
        public string title { get; set; }
        public int mths_since_rcnt_il { get; set; }
        public int total_acc { get; set; }
        public int pub_rec { get; set; }
        public int annual_inc { get; set; }
        public int num_accts_ever_120_pd { get; set; }
        public int max_bal_bc { get; set; }
        public double all_util { get; set; }
        public int mo_sin_old_rev_tl_op { get; set; }
        public int tot_coll_amt { get; set; }
        public int open_rv_24m { get; set; }
        public int total_bal_ex_mort { get; set; }
        public string mths_since_recent_revol_delinq { get; set; }
        public int collections_12_mths_ex_med { get; set; }
        public int chargeoff_within_12_mths { get; set; }
        public int num_rev_tl_bal_gt_0 { get; set; }
    }
}