<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>Snowflake.Data</NuGetReference>
  <Namespace>Snowflake.Data.Client</Namespace>
  <Namespace>System.Data.CData.Snowflake</Namespace>
</Query>

void Main()
{
	string query = "select * from AUDIT_LOG_CASE alc WHERE ALC.ACTION = 'STARS' AND(ALC.DESCRIPTION LIKE '%message sent failed to STARS%')";
	string connectionString = Environment.GetEnvironmentVariable("SF_Con");
	using (IDbConnection conn = new SnowflakeDbConnection())
	{
		conn.ConnectionString = connectionString;
		conn.Open();
		IDbCommand cmd = conn.CreateCommand();
		cmd.CommandText = query;
		 IDataReader reader = cmd.ExecuteReader();

		while (reader.Read())
		{
			Console.WriteLine(reader.GetString(0));
		}
		conn.Close();
	}
}

class sqls
{
	public static string[] sql = {
	"select *  from cpce.transaction   where s_phone_number = '31684741359'  --and send_agent_country = 'NL' with ur",
	"SELECT EXTERNAL_TXN_KEY,MTCN,MTCN10,SEND_DATE,SEND_US_PRINCIPAL,SEND_LOCAL_PRINCIPAL  ,PAY_DATE,PAY_US_PRINCIPAL,PAY_LOCAL_PRINCIPAL,S_GALACTIC_ID,P_GALACTIC_ID, SEND_AGENT_COUNTRY,PAY_AGENT_COUNTRY  ,RERESOLVE_FLAG,TXN_STATUS  FROM CPCE.TRANSACTION  WHERE S_ID_NUMBER = '19293'  WITH UR",
	"select     c.Case_id,     at.attachment_type_description as Attachment_File_Name,     ca.file_type as attachment_type,     ca.description as Attachment_description,     ca.file_name as attachment_name,u.manager,cd.PROCESSING_STAGE as PROCESSING_STAGE,     c.create_timestamp as CASE_START_DATE,c.COMPLETED_TIMESTAMP as Case_end_date,     CT.COMMENT_TYPE_DESCRIPTION as COMMENT_TYPE_DESCRIPTION,     CC.COMMENT as COMMENT,     CC.create_timestamp as CommentAddeddate,     cc.create_user_id as CommentAddedUser,     (Uu.FIRST_NAME || ' ' || Uu.LAST_NAME) AS Commententereduser,     case when foco_user_id is null then concat(concat(u1.first_name,' ' ),u1.last_name) else concat(concat(u.first_name,' ' ),u.last_name) end as ReviewName,     interview_type_description,     case_interview_id,     (Select max(CALLER_LOCAL_DATETIME) from cpce.case_appointment ca where ca.case_key = c.case_key    and ca.appoinment_status = 'Completed')  as Review_Date,     min(al1.Create_Timestamp) as BOCOAssigned,     max(al2.Create_Timestamp) as BOCOCompleted,     max(al2.Create_Timestamp) as FOCOCompleted,     cn.country_name as Country,     ig.inv_grp_name as Investigative_Group,     rs.referral_source_description as Reason,     rl.Risk_Level_Description as LevelAssigned,     concat(concat(u3.first_name,' ' ),u3.last_name) as BOCOUser,     al2.ACTION as ACTION,     cs.Subject_ID     from cpce.case c    left join cpce.case_subject cs on c.case_key = cs.case_key    left join cpce.subject sb on cs.subject_key = sb.subject_key    left join cpce.country cn on sb.country_key = cn.country_key    left join cpce.case_attachment ca on c.case_key = ca.case_key     left join cpce.ATTACHMENT_TYPE AT on ca.attachment_Type_id = AT.attachment_Type_id    left join cpce.case_Details cd on c.case_key = cd.case_key    left join cpce.investigative_group ig on c.inv_grp_id = ig.inv_grp_id    left join cpce.User u on cd.foco_user_id = u.user_id    left join cpce.User u1 on c.assigned_to_user_id = u1.user_id    left join cpce.User u2 on c.modified_user_id = u2.user_id    left join cpce.User u3 on cd.Boco_user_ID = u3.user_id    left join cpce.status st on c.case_status_id = st.status_id    inner join cpce.audit_log_case al1 on c.case_key = al1.case_key and al1.action in ('BO Analyst Assigned','FO Analyst Assigned')     inner join cpce.audit_log_case al2 on c.case_key = al2.case_key and al2.action in ('BO Analyst Completed', 'BO Case Complete','FO Analyst Completed', 'FO Case Complete')    and al1.action_by_user_id not in (select user_ID from cpce.user where manager in ('1000001086','6549','1000001362'))    left join cpce.source_of_referral rs on cd.referral_source_id = rs.referral_source_id    left join cpce.Risk_level rl on cd.risk_level_code = rl.risk_level_code    left join cpce.case_interview ci on ci.case_key = c.case_key    left join cpce.interview_type it on it.interview_type_id = ci.interview_type_id    left outer join CPCE.CASE_COMMENT CC on CC.CASE_KEY = C.CASE_KEY    left outer join CPCE.COMMENT_TYPE CT ON CT.COMMENT_TYPE_ID=CC.COMMENT_TYPE_ID    inner join cpce.user uu on uu.user_id = cc.create_user_id           where c.Business_Group_ID = 'GFO'    and rl.risk_level_description not in ('Cancelled')    group by c.Case_id, c.case_key,ca.file_type,ca.file_name,u.manager,CT.COMMENT_TYPE_DESCRIPTION,CC.COMMENT,cd.PROCESSING_STAGE,c.COMPLETED_TIMESTAMP,c.create_timestamp,    FINAL_SCORE,    case when foco_user_id is null then concat(concat(u1.first_name,' ' ),u1.last_name) else concat(concat(u.first_name,' '),u.last_name) end     ,interview_type_description,    case_interview_id,    ca.description,    ig.inv_grp_name ,at.attachment_type_description,    rs.referral_source_description,cn.country_name,    rl.Risk_Level_Description ,CC.create_timestamp,cc.create_user_id,uu.First_name,uu.Last_name,     concat(concat(u3.first_name,' ' ),u3.last_name) ,    al2.ACTION ,     cs.Subject_ID     Having ( (max(al2.Create_Timestamp) >= '2016-09-01 00:00:00' )    AND (max(al2.Create_Timestamp) <= '2019-09-01 23:59:59' ) and al2.ACTION in ('BO Analyst Completed', 'BO Case Complete')  )     or ( (max(al2.Create_Timestamp) >= '2016-09-01 00:00:00' )    AND (max(al2.Create_Timestamp) <= '2019-09-01 23:59:59' ) and al2.ACTION in ('FO Analyst Completed', 'FO Case Complete'))    with ur"
	};
}
// Define other methods and classes here