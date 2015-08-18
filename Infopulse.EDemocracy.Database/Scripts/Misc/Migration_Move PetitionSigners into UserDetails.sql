set identity_insert dbo.UserDetail on

insert into dbo.UserDetail
(
	ID,
	UserID,
	FirstName,
	MiddleName,
	LastName,
	ZipCode,
	AddressLine1,
	AddressLine2,
	City,
	Region,
	Country,
	CreatedBy,
	CreatedDate,
	ModifiedBy,
	ModifiedDate
)
select
	p.ID,
	u.Id,
	p.FirstName,
	p.MiddleName,
	p.LastName,
	'00000',
	p.AddressLine1,
	p.AddressLine2,
	p.City,
	p.Region,
	p.Country,
	p.CreatedBy,
	p.CreatedDate,
	p.ModifiedBy,
	p.ModifiedDate
from auth.App_User u 
	outer apply
	(
		select top 1
			*
		from dbo.PetitionSigner ps
		where
			ps.Email = u.Email
	) p
	
	

set identity_insert dbo.UserDetail off


select * from dbo.UserDetail