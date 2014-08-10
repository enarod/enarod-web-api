namespace Infopulse.EDemocracy.Model.Common
{
    public static class EntityDictionary
    {
        public static class Agreement
        {
            public static class Status
            {
                public const string Active = "P_Agreement_Status_Active";
                public const string Inactive = "P_Agreement_Status_Inactive";
            }
        }

        public static class Certificate
        {
            public static class Type
            {
                public const string UACrypto = "P_Certificate_Type_UACrypto";
                public const string DPA = "P_Certificate_Type_DPA";
            }
        }

        public static class Petition
        {
            public static class Category
            {
                public const string HealthCare = "P_Petition_Category_HealthCare";
                public const string Military = "P_Petition_Category_Military";
                public const string City = "P_Petition_Category_City";
                public const string Economic = "P_Petition_Category_Economic";
                public const string OutherPolitic = "P_Petition_Category_OutherPolitic";
                public const string Culture = "P_Petition_Category_Culture";
                public const string Education = "P_Petition_Category_Education";
                public const string PeopleRulesViolation = "P_Petition_Category_PeopleRulesViolation";
                public const string Housing = "P_Petition_Category_Housing";
                public const string Transport = "P_Petition_Category_Transport";
                public const string Ecology = "P_Petition_Category_Ecology";
                public const string ArrangeState = "P_Petition_Category_ArrangeState";
                public const string Etc = "P_Petition_Category_Etc";
            }
        }
    }
}