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

Create Procedure AddResult
	@Game NVarChar(50),
	@DollarsPerTierCredit Int,
	@DollarsPerCredit Float,
	@HandsPlayed Int,
	@StartCredits Float,
	@EndCredits Float,
	@TotalHandsPlayed Int,
	@CoinIn Float,
	@TierCreditsEarned Int
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

	Select @ConfigID = Id From Config Where GameId=@GameID and DollarsPerCredit = @DollarsPerCredit and HandsPlayed = @HandsPlayed

	IF(@ConfigID is null)
	BEGIN
		Insert Config(GameId, DollarsPerCredit, HandsPlayed)
			Values(@GameID, @DollarsPerCredit, @HandsPlayed)
		set @ConfigID = @@IDENTITY
	END

	Insert Result(ConfigId, StartCredits, EndCredits, HandsPlayed, CoinIn, TierCreditsEarned)
		Values(@ConfigID, @StartCredits, @EndCredits, @TotalHandsPlayed, @CoinIn, @TierCreditsEarned)
END


