
--DROP PROCEDURE [dbo].[DerivePlayerStatsCareer]

CREATE PROCEDURE dbo.DerivePlayerStatsCareer
	@DryRun int = 0
AS
BEGIN TRY
	SET NOCOUNT ON
/*
-- START comment this out when saving as stored proc
	DECLARE @DryRun int;

	SET @DryRun = 0;
-- STOP comment this out when saving as stored proc
*/

	IF OBJECT_ID('tempdb..#results') IS NOT NULL DROP TABLE #results
	IF OBJECT_ID('tempdb..#playerStatCareersCopy') IS NOT NULL DROP TABLE #playerStatCareersCopy
	IF OBJECT_ID('tempdb..#playerStatCareersNew') IS NOT NULL DROP TABLE #playerStatCareersNew

	CREATE TABLE #results (
		TableName nvarchar(35) NOT NULL,
		NewRecordsInserted int NOT NULL,
		ExistingRecordsUpdated int NOT NULL,
		ProcessedRecordsMatchExistingRecords int NOT NULL
	)

	CREATE TABLE #playerStatCareersNew (
		PlayerId int NOT NULL,
		Playoffs bit NOT NULL,
		Games int NOT NULL,
		Goals int NOT NULL,
		Assists int NOT NULL,
		Points int NOT NULL,
		PenaltyMinutes int NOT NULL,
		PowerPlayGoals int NOT NULL,
		ShortHandedGoals int NOT NULL,
		GameWinningGoals int NOT NULL,
		BCS int NULL
	)
	CREATE UNIQUE INDEX PK ON #playerStatCareersNew(PlayerId, Playoffs)

	CREATE TABLE #playerStatCareersCopy (
		PlayerId int NOT NULL,
		Playoffs bit NOT NULL,
		Games int NOT NULL,
		Goals int NOT NULL,
		Assists int NOT NULL,
		Points int NOT NULL,
		PenaltyMinutes int NOT NULL,
		PowerPlayGoals int NOT NULL,
		ShortHandedGoals int NOT NULL,
		GameWinningGoals int NOT NULL,
		BCS int NULL
	)
	CREATE UNIQUE INDEX PK ON #playerStatCareersCopy(PlayerId, Playoffs)

	INSERT INTO #results
	SELECT
		'PlayerStatCareers' as TableName,
		0 as NewRecordsInserted,
		0 as ExistingRecordsUpdated,
		0 as ProcessedRecordsMatchExistingRecords

	-- 'Count Player Stats Careers'
	insert into #playerStatCareersNew
	select
		s.PlayerId,
		s.Playoffs,
		count(s.GameId) as Games,
		sum(s.Goals) as Goals,
		sum(s.Assists) as Assists,
		sum(s.Points) as Points,
		sum(s.PenaltyMinutes) as PenaltyMinutes,
		sum(s.PowerPlayGoals) as PowerPlayGoals,
		sum(s.ShortHandedGoals) as ShortHandedGoals,
		sum(s.GameWinningGoals) as GameWinningGoals,
		NULL as BCS
	from
		PlayerStatGames s
	where
		s.PlayerId <> 0
	group by
		s.PlayerId,
		s.Playoffs


	update #playerStatCareersNew
	set
		BCS = BINARY_CHECKSUM(PlayerId,
								Playoffs,
								Games,
								Goals,
								Assists,
								Points,
								PenaltyMinutes,
								PowerPlayGoals,
								ShortHandedGoals,
								GameWinningGoals)


	-- 'Count Copying PlayerStatCareers'
	INSERT INTO #playerStatCareersCopy
	SELECT 
		PlayerId,
		Playoffs,
		Games,
		Goals,
		Assists,
		Points,
		PenaltyMinutes,
		PowerPlayGoals,
		ShortHandedGoals,
		GameWinningGoals,
		BINARY_CHECKSUM(PlayerId,
								Playoffs,
								Games,
								Goals,
								Assists,
								Points,
								PenaltyMinutes,
								PowerPlayGoals,
								ShortHandedGoals,
								GameWinningGoals) as BCS
	FROM 
		PlayerStatCareers


	IF (@dryrun = 1) 
	BEGIN
		-- this is not a dry run
		PRINT 'DRY RUN. NOT UPDATING REAL TABLES'

		update #playerStatCareersCopy
		set
			Games = n.Games,
			Goals = n.Goals,
			Assists = n.Assists,
			Points = n.Points,
			PenaltyMinutes = n.PenaltyMinutes,
			PowerPlayGoals = n.PowerPlayGoals,
			ShortHandedGoals = n.ShortHandedGoals,
			GameWinningGoals = n.GameWinningGoals
		from
			#playerStatCareersCopy c INNER JOIN
			#playerStatCareersNew n ON (c.PlayerId = n.PlayerId AND c.Playoffs = n.Playoffs)
		where
			c.BCS <> n.BCS

		update #results set ExistingRecordsUpdated = @@ROWCOUNT

		insert into #playerStatCareersCopy
		select
			n.*
		from
			#playerStatCareersNew n left join
			#playerStatCareersCopy c on (c.PlayerId = n.PlayerId AND c.Playoffs = n.Playoffs)
		where
			c.PlayerId is null

		update #results set NewRecordsInserted = @@ROWCOUNT
	END
	ELSE
	BEGIN
		-- this is not a dry run
		PRINT 'NOT A DRY RUN. UPDATING REAL TABLES'

		update PlayerStatCareers
		set
			Games = n.Games,
			Goals = n.Goals,
			Assists = n.Assists,
			Points = n.Points,
			PenaltyMinutes = n.PenaltyMinutes,
			PowerPlayGoals = n.PowerPlayGoals,
			ShortHandedGoals = n.ShortHandedGoals,
			GameWinningGoals = n.GameWinningGoals
		from
			PlayerStatCareers r INNER JOIN
			#playerStatCareersCopy c ON (r.PlayerId = c.PlayerId AND r.Playoffs = c.Playoffs) INNER JOIN
			#playerStatCareersNew n ON (c.PlayerId = n.PlayerId AND c.Playoffs = n.Playoffs)
		where
			c.BCS <> n.BCS

		update #results set ExistingRecordsUpdated = @@ROWCOUNT

		insert into PlayerStatCareers
		select
			n.PlayerId,
			n.Playoffs,
			n.Games,
			n.Goals,
			n.Assists,
			n.Points,
			n.PenaltyMinutes,
			n.PowerPlayGoals,
			n.ShortHandedGoals,
			n.GameWinningGoals,
			GETDATE()
		from
			#playerStatCareersNew n left join
			PlayerStatCareers c on (c.PlayerId = n.PlayerId AND c.Playoffs = n.Playoffs)
		where
			c.PlayerId is null

		update #results set NewRecordsInserted = @@ROWCOUNT

	END

	update #results set ProcessedRecordsMatchExistingRecords = (select count(*) from #playerStatCareersNew) - NewRecordsInserted - ExistingRecordsUpdated

	select * from #results
END TRY
BEGIN CATCH
    THROW;
END CATCH;
