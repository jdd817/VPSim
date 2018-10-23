create database vp
go
use vp
GO

create table Game
(
	Id Int Not Null Identity Primary Key,
	Name NVarChar(50) Not Null,
	DollarsPerTierCredit Int Not Null
)

Create Table Config
(
	Id Int Not Null Identity Primary Key,
	GameId Int Not Null,
	DollarsPerCredit Float Not Null,
	HandsPlayed Int Not Null,
        Target Int,
        Bankroll Int,
	Constraint FK_Config_Game Foreign Key(GameId) References Game(Id)
)

Create Table Result
(
	Id Int Not Null Identity Primary Key,
	ConfigId Int Not Null,
	StartCredits Float,
	EndCredits Float,
	HandsPlayed Int,
	CoinIn Float,
	TierCreditsEarned Int,
	Constraint FK_Result_Config Foreign Key(ConfigId) References Config(Id)
)

go

CREATE Procedure [dbo].[AddResult]
	@Game NVarChar(50),
	@DollarsPerTierCredit Int,
	@DollarsPerCredit Float,
	@HandsPlayed Int,
	@StartCredits Float,
	@EndCredits Float,
	@TotalHandsPlayed Int,
	@CoinIn Float,
	@TierCreditsEarned Int,
	@Bankroll Int,
	@TargetPoints Int
AS
BEGIN

	Declare @GameID Int, @ConfigID Int

	Select @GameId = Id From Game Where Name=@Game and DollarsPerTierCredit=@DollarsPerTierCredit

	if(@GameID Is Null)
	BEGIN
		Insert Game(Name, DollarsPerTierCredit)
			Values(@Game, @DollarsPerTierCredit)
		set @GameID = @@IDENTITY
	END

	Select @ConfigID = Id From Config Where GameId=@GameID and DollarsPerCredit = @DollarsPerCredit and HandsPlayed = @HandsPlayed and Bankroll = @Bankroll and Target = @TargetPoints

	IF(@ConfigID is null)
	BEGIN
		Insert Config(GameId, DollarsPerCredit, HandsPlayed, Bankroll, Target)
			Values(@GameID, @DollarsPerCredit, @HandsPlayed, @Bankroll, @TargetPoints)
		set @ConfigID = @@IDENTITY
	END

	Insert Result(ConfigId, StartCredits, EndCredits, HandsPlayed, CoinIn, TierCreditsEarned)
		Values(@ConfigID, @StartCredits, @EndCredits, @TotalHandsPlayed, @CoinIn, @TierCreditsEarned)
END




GO

CREATE View [dbo].[VpResults]
as
select *, DollarsPerCredit * 5 * HandsPlayed as DollarsPerHand, HandsNeeded * (10/60.0) as EstMinutesNeeded, HandsNeeded * (10/3600.0) as EstHoursNeeded from
(
	select Name + ' $'+ Convert(NvarChar(20), DollarsPerCredit) + ' x '+ConverT(NvarChar(50), c.HandsPlayed) as Game, 
		g.Name, 
		c.DollarsPerCredit, 
		c.HandsPlayed,		
		c.Bankroll,
		c.Target,
		Avg(EndCredits) as Avg, STDEV(EndCredits) as StdDev,
		count(*) as Trials,
		Ceiling(c.Target/((c.DollarsPerCredit * 5 * c.HandsPlayed)/g.DollarsPerTierCredit)) as HandsNeeded,
		(select count(*)*1.0 from result rr where rr.ConfigId=c.Id and rr.EndCredits = 0)/(select count(*)*1.0 from result rr where rr.ConfigId=c.Id) * 100 as RiskOfRuin
	from game g
	join config c on c.GameId=g.id
	join result r on r.ConfigId=c.Id
	Where r.EndCredits<=r.StartCredits*2
	group by g.Name, c.id, c.DollarsPerCredit, c.HandsPlayed, g.DollarsPerTierCredit, c.Bankroll, c.Target
) a	---order by g.Name, c.DollarsPerCredit, c.HandsPlayed
where trials>100

GO

CREATE View [dbo].[VpGames]
AS
	select g.id as GameId, c.id as ConfigId, g.Name, c.DollarsPerCredit, c.HandsPlayed, Name + ' $'+ Convert(NvarChar(20), DollarsPerCredit) + ' x '+ConverT(NvarChar(50), HandsPlayed) as Game
	from game g
	join config c on c.GameId=g.id
GO


