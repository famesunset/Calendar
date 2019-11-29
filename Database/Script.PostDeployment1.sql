IF (SELECT COUNT(*) FROM Color) = 0
	INSERT INTO Color VALUES ('#fff'), ('#ccc')

IF (SELECT COUNT(*) FROM Repeat) = 0
	INSERT INTO Repeat VALUES (0, 'No-repeat'), (1, 'Every day'), (7, 'Every week'), (30, 'Every month')

IF (SELECT COUNT(*) FROM Access) = 0
	INSERT INTO Access VALUES ('Private')