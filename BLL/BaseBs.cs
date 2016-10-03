using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Security;
using System.Configuration;
using DAL;

namespace BLL {
    public class BaseBs {
        public RoleBs roleBs { get; set; }
        public TUserBs tuserBs { get; set; }
        public DOM_CMD_VENDORBs dOM_CMD_VENDORBs { get; set; }
        public DOM_MDS_ORGANIZATIONBs dOM_MDS_ORGANIZATIONBs { get; set; }
        public DOM_SAP_MATFRIGRPBs dOM_SAP_MATFRIGRPBs { get; set; }
        public DWH_ONTIME_DNBs dWH_ONTIME_DNBs { get; set; }
        public DWH_ONTIME_SHIPMENTBs dWH_ONTIME_SHIPMENTBs { get; set; }
        public HomeKPIBs HomeKPIBs { get; set; }
        public KPIBs kPIBs { get; set; }
        public MenuTableBs menuTableBs { get; set; }
        //accepted
        public OntimeAcceptBs ontimeAcceptBs { get; set; }
        public OntimeAcceptMonthBs ontimeAcceptMonthBs { get; set; }
        public OntimeAcceptYearBs ontimeAcceptYearBs { get; set; }
        public ReasonAcceptedBs reasonAcceptedBs { get; set; }
        public AcceptedDelayBs acceptedDelayBs { get; set; }
        public AcceptPendingBs acceptPendingBs { get; set; }
        public AcceptedAdjustedBs acceptedAdjustedBs { get; set; }
        public AcceptedFilesBs acceptedFilesBs { get; set; }
        //tendered
        public OntimeTenderBs ontimeTenderBs { get; set; }
        public OntimeTenderMonthBs ontimeTenderMonthBs { get; set; }
        public ReasonTenderedBs reasonTenderedBs { get; set; }
        public TenderedDelayBs tenderedDelayBs { get; set; }
        public OntimeTenderYearBs ontimeTenderYearBs { get; set; }
        public TenderedPendingBs tenderedPendingBs { get; set; }
        public TenderedAdjustedBs tenderedAdjustedBs { get; set; }
        public TenderedFilesBs tenderedFilesBs { get; set; }
        //inbounded
        public OntimeInboundBs ontimeInboundBs { get; set; }
        public OntimeInboundMonthBs ontimeInboundMonthBs { get; set; }
        public OntimeInboundYearBs ontimeInboundYearBs { get; set; }
        public InboundDelayBs inboundDelayBs { get; set; }
        public ReasonInboundBs reasonInboundBs { get; set; }
        public InboundPendingBs inboundPendingBs { get; set; }
        public InboundAdjustedBs inboundAdjustedBs { get; set; }
        public InboundFilesBs inboundFilesBs { get; set; }
        //outbound
        public OntimeOutboundBs ontimeOutboundBs { get; set; }
        public OntimeOutboundMonthBs ontimeOutboundMonthBs { get; set; }
        public OntimeOutboundYearBs ontimeOutboundYearBs { get; set; }
        public OutboundDelayBs outboundDelayBs { get; set; }
        public ReasonOutboundBs reasonOutboundBs { get; set; }
        public OutboundPendingBs outboundPendingBs { get; set; }
        public OutboundAdjustedBs outboundAdjustedBs { get; set; }
        public OutboundFilesBs outboundFilesBs { get; set; }
        //doc return
        public OntimeDocReturnBs ontimeDocReturnBs { get; set; }
        public OntimeDocReturnMonthBs ontimeDocReturnMonthBs { get; set; }
        public OntimeDocReturnYearBs ontimeDocReturnYearBs { get; set; }
        public DocReturnDelayBs docReturnDelayBs { get; set; }
        public ReasonDocReturnBs reasonDocReturnBs { get; set; }
        public DocReturnPendingBs docReturnPendingBs { get; set; }
        public DocReturnAdjustedBs docReturnAdjustedBs { get; set; }
        public DocReturnFilesBs docReturnFilesBs { get; set; }
        //delivery
        public OntimeDeliveryBs ontimeDeliveryBs { get; set; }
        public OntimeDeliveryMonthBs ontimeDeliveryMonthBs { get; set; }
        public OntimeDeliveryYearBs ontimeDeliveryYearBs { get; set; }
        public OntimeDelayBs ontimeDelayBs { get; set; }
        public ReasonOntimeBs reasonOntimeBs { get; set; }
        public OntimePendingBs ontimePendingBs { get; set; }
        public OntimeAdjustedBs ontimeAdjustedBs { get; set; }
        public OntimeFilesBs ontimeFilesBs { get; set; }

        public BaseBs() {
            roleBs = new RoleBs();
            tuserBs = new TUserBs();
            dOM_CMD_VENDORBs = new DOM_CMD_VENDORBs();
            dOM_MDS_ORGANIZATIONBs = new DOM_MDS_ORGANIZATIONBs();
            dOM_SAP_MATFRIGRPBs = new DOM_SAP_MATFRIGRPBs();
            dWH_ONTIME_DNBs = new DWH_ONTIME_DNBs();
            dWH_ONTIME_SHIPMENTBs = new DWH_ONTIME_SHIPMENTBs();
            menuTableBs = new MenuTableBs();
            HomeKPIBs = new HomeKPIBs();

            //tendered
            ontimeTenderBs = new OntimeTenderBs();
            ontimeTenderMonthBs = new OntimeTenderMonthBs();
            ontimeTenderYearBs = new OntimeTenderYearBs();
            reasonTenderedBs = new ReasonTenderedBs();
            tenderedDelayBs = new TenderedDelayBs();
            tenderedPendingBs = new TenderedPendingBs();
            tenderedAdjustedBs = new TenderedAdjustedBs();
            tenderedFilesBs = new TenderedFilesBs();

            //accepted
            ontimeAcceptBs = new OntimeAcceptBs();
            ontimeAcceptMonthBs = new OntimeAcceptMonthBs();
            reasonAcceptedBs = new ReasonAcceptedBs();
            ontimeAcceptYearBs = new OntimeAcceptYearBs();
            acceptedDelayBs = new AcceptedDelayBs();
            acceptPendingBs = new AcceptPendingBs();
            acceptedAdjustedBs = new AcceptedAdjustedBs();
            acceptedFilesBs = new AcceptedFilesBs();

            //inbounded
            ontimeInboundBs = new OntimeInboundBs();
            ontimeInboundMonthBs = new OntimeInboundMonthBs();
            ontimeInboundYearBs = new OntimeInboundYearBs();
            inboundDelayBs = new InboundDelayBs();
            reasonInboundBs = new ReasonInboundBs();
            inboundPendingBs = new InboundPendingBs();
            inboundAdjustedBs = new InboundAdjustedBs();
            inboundFilesBs = new InboundFilesBs();

            //outbound
            ontimeOutboundBs = new OntimeOutboundBs();
            ontimeOutboundMonthBs = new OntimeOutboundMonthBs();
            ontimeOutboundYearBs = new OntimeOutboundYearBs();
            outboundDelayBs = new OutboundDelayBs();
            reasonOutboundBs = new ReasonOutboundBs();
            outboundPendingBs = new OutboundPendingBs();
            outboundAdjustedBs = new OutboundAdjustedBs();
            outboundFilesBs = new OutboundFilesBs();

            //doc returnt
            ontimeDocReturnBs = new OntimeDocReturnBs();
            ontimeDocReturnMonthBs = new OntimeDocReturnMonthBs();
            ontimeDocReturnYearBs = new OntimeDocReturnYearBs();
            docReturnDelayBs = new DocReturnDelayBs();
            reasonDocReturnBs = new ReasonDocReturnBs();
            docReturnPendingBs = new DocReturnPendingBs();
            docReturnAdjustedBs = new DocReturnAdjustedBs();
            docReturnFilesBs = new DocReturnFilesBs();

            //delivery
            ontimeDeliveryBs = new OntimeDeliveryBs();
            ontimeDeliveryMonthBs = new OntimeDeliveryMonthBs();
            ontimeDeliveryYearBs = new OntimeDeliveryYearBs();
            ontimeDelayBs = new OntimeDelayBs();
            reasonOntimeBs = new ReasonOntimeBs();
            ontimePendingBs = new OntimePendingBs();
            ontimeAdjustedBs = new OntimeAdjustedBs();
            ontimeFilesBs = new OntimeFilesBs();
        }
    }


    public class SCGLKPIMembershipProvider : MembershipProvider {
        TUserDb db;
        public SCGLKPIMembershipProvider() {
            db = new TUserDb();
        }
        public override string ApplicationName {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword) {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData) {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline() {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline) {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email) {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer) {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName) {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user) {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password) {
            int count = db.GetAll().Where(x => x.UserEmail == username && x.Password == password).Count();
            if (count != 0)
                return true;
            else
                return false;
        }
    }
    public class SCGLKPIRoleProvider : RoleProvider {
        TUserDb db;
        public SCGLKPIRoleProvider() {
            db = new TUserDb();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override string ApplicationName {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName) {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles() {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username) {
            string[] s = { db.GetAll().Where(x => x.UserEmail == username).FirstOrDefault().RoleId };
            return s;
        }

        public override string[] GetUsersInRole(string roleName) {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName) {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName) {
            throw new NotImplementedException();
        }
    }
}