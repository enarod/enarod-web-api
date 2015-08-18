create table dbo.UserDetail
(
	ID int not null identity(1, 1),
	UserID int not null,
	FirstName nvarchar(300),
	MiddleName nvarchar(300),
	LastName nvarchar(300),
	ZipCode nvarchar(10) default '00000',
	AddressLine1 nvarchar(4000),
	AddressLine2 nvarchar(4000),
	City nvarchar(300),
	Region nvarchar(300),
	Country nvarchar(300),
	CreatedBy nvarchar(4000) not null default 'Unknown DB user',
	CreatedDate datetime2 not null default getutcdate(),
	ModifiedBy nvarchar(4000),
	ModifiedDate datetime2,
	constraint PK_UserDetail primary key (ID),
	constraint FK_UserDetail_Auth_App_User foreign key (UserID) references auth.App_User(ID)
)