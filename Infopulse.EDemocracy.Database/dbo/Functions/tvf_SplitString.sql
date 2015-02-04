create function [dbo].[tvf_SplitString]
(
    @String nvarchar(max), 
    @Separator char(1) = ','
)
returns table
as
return (
with cte_tokens(p, a, b) as (
    select 
        cast(1 as bigint), 
        cast(1 as bigint), 
        charindex(@Separator, @String)
    union all
    select
        p + 1, 
        b + 1, 
        charindex(@Separator, @String, b + 1)
    from cte_tokens
    where b > 0
)
select
    p as WordIndex,
    rtrim(ltrim(substring(
        @String, 
        a, 
        case when b > 0 then b-a ELSE LEN(@String) end)))
    as Word
from cte_tokens
);