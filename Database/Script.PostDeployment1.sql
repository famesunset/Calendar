IF (SELECT COUNT(*) FROM Color) = 0
	INSERT INTO Color VALUES ('#9E69AF'), ('#D50000'), ('#E67C73'), ('#F4511E'), ('#F6BF26'), ('#33B679'), ('#0B8043'), 
	('#039BE5'), ('#3F51B5'), ('#7986CB'), ('#8E24AA'), ('#616161')

IF (SELECT COUNT(*) FROM Repeat) = 0
	INSERT INTO Repeat VALUES (0, 'No-repeat'), (1, 'Every day'), (7, 'Every week'), (30, 'Every month')

IF (SELECT COUNT(*) FROM Access) = 0
	INSERT INTO Access VALUES ('Private')