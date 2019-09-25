select			*
from			Friends		as f
where			(1=1)
		and		MONTH(f.BirthDate) >= MONTH(GETDATE())
		and		DAY(f.BirthDate) <= DAY(GETDATE())
order by		MONTH(f.BirthDate) 
		,		DAY(f.BirthDate)
ASC