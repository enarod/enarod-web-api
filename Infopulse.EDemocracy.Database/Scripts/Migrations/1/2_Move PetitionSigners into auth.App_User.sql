begin tran

if object_id('tempdb..#User', 'U') is not null
drop table #User

select *
into #User
from auth.App_User

select * from #User

if object_id('tempdb..#PetitionSigner', 'U') is not null
drop table #PetitionSigner

select distinct ps.Email
into #PetitionSigner
from dbo.PetitionSigner ps
where
	len(ps.Email) > 0
	and ps.Email not like 'demouser%'
	and ps.Email not like 'aregaz_@%'
	and ps.Email <> 'illia.ratkevych@gmail.com'

select * from #PetitionSigner

insert into auth.App_User
(
	--Id,
    Email,
    EmailConfirmed,
    PasswordHash,
    SecurityStamp,
    PhoneNumber,
    PhoneNumberConfirmed,
    TwoFactorEnabled,
    LockoutEndDateUtc,
    LockoutEnabled,
    AccessFailedCount,
    UserName
)
select
	ps.Email,
	u.EmailConfirmed,
	u.PasswordHash,
	u.SecurityStamp,
	u.PhoneNumber,
	u.PhoneNumberConfirmed,
	u.TwoFactorEnabled,
	u.LockoutEndDateUtc,
	u.LockoutEnabled,
	u.AccessFailedCount,
	ps.Email
from #PetitionSigner ps,
	#User u

select * from auth.App_User

commit tran