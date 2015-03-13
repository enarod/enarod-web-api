;merge dbo.Settings as target
using
(
	values
	(1, 'ActivePetitionVoteCount', '7', 'Кількість голосів за петицію, яка необхідна для того, щоб петиція вважалась активною.')
) as source (ID, [Key], [Value], [Description])
on target.ID = source.ID

when matched then
	update
	set
		[Key] = source.[Key],
		[Value] = source.[Value],
		[Description] = source.[Description]

when not matched by target then
	insert (ID, [Key], [Value], [Description])
	values (ID, [Key], [Value], [Description])
	
when not matched by source then
	delete;