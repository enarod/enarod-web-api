exec NewEntityGroup null, 'Agreement'
exec NewEntityGroup 1, 'Status'

exec NewEntityGroup null, 'Certificate'
exec NewEntityGroup 3, 'Type'

exec NewEntityGroup null, 'Petition'
exec NewEntityGroup 5, 'Category'

--------------------------------------------------------------

exec NewEntity 2, 'P_Agreement_Status_Active', N'Активна угода'
exec NewEntity 2, 'P_Agreement_Status_Inactive', N'Неактивна угода'

exec NewEntity 4, 'P_Certificate_Type_UACrypto', N'UACrypto'
exec NewEntity 4, 'P_Certificate_Type_DPA', N'DPA'

exec NewEntity 6, 'P_Petition_Category_HealthCare', N'охорона здоров''я'
exec NewEntity 6, 'P_Petition_Category_Military', N'військова сфера'
exec NewEntity 6, 'P_Petition_Category_City', N'благоустрій міста'
exec NewEntity 6, 'P_Petition_Category_Economic', N'економіка'
exec NewEntity 6, 'P_Petition_Category_OutherPolitic', N'зовнішня політика'
exec NewEntity 6, 'P_Petition_Category_Culture', N'культура'
exec NewEntity 6, 'P_Petition_Category_Education', N'освіта і наука'
exec NewEntity 6, 'P_Petition_Category_PeopleRulesViolation', N'порушення прав людини'
exec NewEntity 6, 'P_Petition_Category_Housing', N'ЖКГ'
exec NewEntity 6, 'P_Petition_Category_Transport', N'дорожно-транспортна сфера'
exec NewEntity 6, 'P_Petition_Category_Ecology', N'екологія'
exec NewEntity 6, 'P_Petition_Category_ArrangeState', N'державний лад'
exec NewEntity 6, 'P_Petition_Category_Etc', N'ІНШЕ'
