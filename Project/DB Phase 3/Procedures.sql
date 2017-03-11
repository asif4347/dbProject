create procedure get_account
@uid int,
@block int output
as
begin
	select @block=UoT.block from UoT where UoT.User_ID=@uid
end
go
-------------------------------------------------------------------------
create procedure search_results
@name varchar(50)
as
begin
SELECT User_ID, User_Name as Users
FROM UoT
WHERE User_Name=@name AND block!=1
END
go


-----------------------------------------------------------------------------------------------------------


create procedure create_account
@name varchar (20),
@password varchar (50),
@emailid varchar (50),
@phone_no varchar (50),
@succes int output
AS 
	BEGIN
	if((select UoT.User_ID from UoT where UoT.Email=@emailid or UoT.phone_no=@phone_no) is null)
	begin
	INSERT INTO UoT (User_Name, phone_no, Email, Password, block) values  (@name, @phone_no, @emailid, @password, '0')
	select @succes=UoT.User_ID from UoT where UoT.Email=@emailid
	end
	else
	begin
	set @succes=-1
	end
	END


go


-----------------------------------------------------------------------------------------------------------2








CREATE PROCEDURE get_tweets
@id1 int
AS 
BEGIN
	SELECT Tweet_ID,User_Name, Tweet_Text, Date_Time 
	FROM (UoT as U1
	JOIN
		(Select Tweet_ID, F.User_ID, Tweet_text, Date_Time
		FROM Followers as F
		JOIN
		(	SELECT ot.Tweet_ID, User_ID, Tweet_Text, Date_Time
			FROM org_tweet as ot
			JOIN AllTweets as at
			ON ot.Tweet_ID=at.Tweet_ID
			WHERE block!=1
		) as r2
		ON r2.User_ID=F.User_ID
		WHERE  F.f_ids=@id1) as r1
		ON U1.User_ID=r1.User_ID)

	
		
	
	UNION

	(SELECT Tweet_ID,User_Name, Tweet_text, Date_Time
	FROM UoT as U2
	JOIN 
	(	SELECT ot.Tweet_ID,User_ID, Tweet_Text, Date_Time
		FROM org_tweet as ot
		JOIN AllTweets as at
		ON ot.Tweet_ID=at.Tweet_ID
		WHERE block!=1
		--ORDER BY Date_and_Time DESC
	) as r3
	ON U2.User_ID=r3.User_ID
	WHERE U2.User_ID=@id1)
	

	

END
go
-----------------------------------------------------------------------------------------------------------


CREATE PROCEDURE get_tcount
@id4 int,
@count2 int OUTPUT
AS
BEGIN
	SELECT @count2=count(*)
	FROM AllTweets
	WHERE User_ID=@id4
	if (@count2 is NULL)
	SELECT @count2=0;
END

go
-----------------------------------------------------------------------------------------------------------


CREATE PROCEDURE get_name
@id5 int,
@name varchar (50) OUTPUT
AS
BEGIN
	SELECT @name=User_Name
	FROM UoT
	WHERE  User_ID=@id5
END

go
-----------------------------------------------------------------------------------------------------------

CREATE PROCEDURE get_fcount
@id2 int,
@count int OUTPUT
AS
	BEGIN
	Select @count=count(f_ids)
	FROM UoT as U
	JOIN Followers as F
	ON U.User_ID=F.User_ID
	WHERE F.User_ID=@id2
	GROUP BY U.User_ID
	if (@count is NULL)
	SELECT @count=0;
	
END


go
-----------------------------------------------------------------------------------------------------------




CREATE PROCEDURE get_fby_count
@id3 int,
@count1 int OUTPUT
AS
	BEGIN
	Select @count1=count (F.User_ID) 
	FROM UoT as U
	JOIN Followers as F
	ON U.User_ID=F.User_ID
	WHERE F.f_ids=@id3
	GROUP BY F.f_ids
	if (@count1 is NULL)
	SELECT @count1=0;
	
END 



-----------------------------------------------------------------------------------------------------------
go

create procedure refresh
@id int
AS 
BEGIN
EXECUTE get_tweets @id1=@id
		DECLARE @my_count int
		EXECUTE get_fcount @id2=@id,
		@count=@my_count OUTPUT
		DECLARE @myc int
		EXECUTE get_fby_count @id3=@id,
		@count1=@myc OUTPUT
		SELECT User_Name
		FROM UoT
		WHERE User_ID=@id

END




-----------------------------------------------------------------------------------------------------------
go

CREATE PROCEDURE login_proc
@email varchar (50)=NULL,
@phone_no varchar (50)=NULL,
@password varchar (50), 
@uid int OUTPUT

AS 
BEGIN
	 IF @email is null AND @phone_no is not null
		BEGIN
		SELECT @uid=UoT.User_ID
		FROM UoT
		WHERE @password=Password AND @phone_no=Phone_no
		END
	ELSE
		IF @email is not null AND @phone_no is null
		BEGIN
		SELECT @uid=UoT.User_ID
		FROM UoT
		WHERE @password=Password AND @email=Email
		END
		EXECUTE refresh @id=@uid
END

go


-----------------------------------------------------------------------------------------------------------

CREATE PROCEDURE new_tweet
@u_id int,
@text varchar (250)= 'new tweet',
@t_id int output
AS
	BEGIN
		INSERT INTO org_tweet (Tweet_Text, block) values (@text, 0);
		SELECT @t_id=max(Tweet_ID)
		FROM org_tweet
		INSERT INTO AllTweets values (@t_id, @u_id, CURRENT_TIMESTAMP, 0)
	END

	
	


	go


-----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE Follow
@uid int,
@fid int,
@flag int OUTPUT
AS
	BEGIN
		IF( @uid!=@fid AND NOT EXISTS (SELECT * from Followers WHERE f_ids=@uid AND User_ID=@fid ))	
		BEGIN
			INSERT INTO Followers (User_ID, f_ids) values (@fid, @uid);
			SET @flag=1;

		END
		ELSE
		BEGIN
			SET @flag=0;
		END
	END


	go

-----------------------------------------------------------------------------------------------------------
CREATE PROCEDURE Unfollow
@uid int,
@fid int,
@flag int OUTPUT
AS
BEGIN 
	IF( @uid!=@fid AND EXISTS (SELECT * from Followers WHERE f_ids=@uid AND User_ID=@fid ))	
		BEGIN
		DELETE FROM Followers
		WHERE f_ids=@uid AND User_ID=@fid
		SET @flag=1;
		END
	ELSE
		BEGIN
		SET @flag=0;
		END
		

	END



	go
-----------------------------------------------------------------------------------------------------------
--func 9
create procedure dbo.sendPM
@sendId int,
@recvId int,
@msg varchar(500),
@flag int output
as
begin

if(@recvId in (select f_ids from Followers where Followers.User_ID=@sendId) )
begin
insert into message(Sender_Id,Reciever_Id,Message,[Date and Time]) values(@sendId,@recvId,@msg,CURRENT_TIMESTAMP);
set @flag=1
end
else
begin
set @flag=0
end
end

go
---------------------------------------------------------------------------------
--func 10
create procedure dbo.trendset
@trend_txt varchar(21),
@tweetid int output,
@trendid int output,
@check int output
as
begin
SELECT @check=count(*)
FROM Trend
WHERE Trend_Text=@trend_txt
if(@check=0)
begin
insert into Trend(Trend_Text) values (@trend_txt);
end
select @trendid=Trend_ID from Trend where trend_text=@trend_txt
select @tweetid=max(Tweet_ID) from org_tweet 
insert into Tweet_Trend(Tweet_ID,Trend_ID) values (@tweetid,@trendid)
end

go
---------------------------------------------------------------------------------

--func 11
create procedure dbo.DPlink
@usr int,
@dis varchar(50) output
as
begin
select @dis=DP from UoT
where @usr=UoT.User_ID
end

go
----------------------------------------------------------------------
--func 12
create procedure dbo.retweet
@retweeter int,
@tweetid int 
as
begin


insert into AllTweets(Tweet_ID,User_ID,[Date_Time], Retweet_bit) values (@tweetid,@retweeter,CURRENT_TIMESTAMP, 1)
end

go
----------------------------------------------------------------

--func 13 a
create procedure dbo.BlockTweet
@tweetid int
as
begin
update org_tweet
SET block=1
where Tweet_ID=@tweetid
end




go
-----------------------------------------------------------------
--func 13 b
create procedure dbo.BlockUser
@usr int
as
begin
update UoT set UoT.block=1
where UoT.User_ID=@usr
update ot
set ot.block=1
FROM org_tweet as ot
JOIN AllTweets as A
ON ot.Tweet_ID=A.Tweet_ID
where A.User_ID=@usr


end

--execute dbo.BlockUser @usr=1

---------------------------------------------------------------

go


create procedure get_admin_tweets
as
begin
SELECT r1.Tweet_ID, U.User_Name, r1.Tweet_text, r1.Date_Time
FROM UoT as U
JOIN 
(
select ot.Tweet_ID, User_ID, Tweet_Text, Date_Time
from org_tweet as ot
join AllTweets as a
ON ot.Tweet_ID=a.Tweet_ID
WHERE ot.block!=1
) as  r1
ON U.User_ID=r1.User_ID
end




go
---------------------------------------------------------------

create procedure delete_account
@id int
AS 
	BEGIN
	DELETE 
	FROM UoT
	WHERE @id=User_ID
	END
go
-----------------------------------------------------------
create procedure is_admin
@id int,
@flag int output
as
begin
if(@id in (select Admin.User_ID from Admin))
begin
set @flag=1
end
else
begin
set @flag=0
end
end

--------------------------------------------------------------
create procedure get_yourtweets
@uid int
as
begin
select Tweet_text, Date_Time
from org_tweet join AllTweets
on org_tweet.Tweet_ID=AllTweets.Tweet_ID
where AllTweets.User_ID=@uid
end

--------------------------------------------------------------
create procedure get_yourfollowers
@uid int
as 
begin
select UoT.User_Name as Followers
from UoT JOIN Followers
on UoT.User_ID=Followers.f_ids
where Followers.User_ID=@uid
end
---------------------------------------------------------------
create procedure search_trends
@tren varchar (50)
as
begin
select Trend.trend_text,UoT.User_Name,org_tweet.Tweet_text,AllTweets.Date_Time
from Trend join tweet_trend
on Trend.Trend_ID=tweet_trend.Trend_ID
join AllTweets
on tweet_trend.Tweet_ID=AllTweets.Tweet_ID
join org_tweet
on org_tweet.Tweet_ID=AllTweets.Tweet_ID
join UoT
on UoT.User_ID=AllTweets.User_ID
where Trend.trend_text=@tren
end
---------------------------------------------------------------
create procedure msg_reciever
@uid int
as 
begin
select message.Message As Message, UoT.User_Name as Sender , message.[Date and Time]
from message join UoT
on message.Sender_Id=UoT.User_ID
where Reciever_Id = @uid
end
--------------------------------------------------------------
create procedure youfollowing
@uid int
as
begin
select UoT.User_Name as Following
from UoT JOIN Followers
on Followers.User_ID=UoT.User_ID
where Followers.f_ids=@uid
end




---------------------------------------------------------------
Function
---------------------------------------------------------------
create function gettrends(@tren varchar(50))
returns table
as
return 
(
	select Trend.trend_text,UoT.User_Name,org_tweet.Tweet_text,AllTweets.Date_Time
	from Trend join tweet_trend
	on Trend.Trend_ID=tweet_trend.Trend_ID
	join AllTweets
	on tweet_trend.Tweet_ID=AllTweets.Tweet_ID
	join org_tweet
	on org_tweet.Tweet_ID=AllTweets.Tweet_ID
	join UoT
	on UoT.User_ID=AllTweets.User_ID
	where Trend.trend_text=@tren
)



INSERT INTO UoT (User_Name, DP, DoB, Phone_no, Email, Password, block) values ('Ahmad', 'no', '1/2/2014', '1234', 'hey@gmail.com', 'hello', 0);
INSERT INTO UoT (User_Name, DP, DoB, Phone_no, Email, Password, block) values ('Ali', 'no', '1/2/2014', '12345', 'heya@gmail.com', 'hello', 0);
INSERT INTO UoT (User_Name, DP, DoB, Phone_no, Email, Password, block) values ('Umar', 'no', '1/2/2014', '12346', 'heyb@gmail.com', 'hello', 0);
INSERT INTO UoT (User_Name, DP, DoB, Phone_no, Email, Password, block) values ('Ashfaq', 'no', '1/2/2014', '12347', 'heyc@gmail.com', 'hello', 0);
INSERT INTO UoT (User_Name, DP, DoB, Phone_no, Email, Password, block) values ('Azhar', 'no', '1/2/2014', '12348', 'heyd@gmail.com', 'hello', 0);
	
INSERT INTO org_tweet (Tweet_Text, block) values ('hello there', 0);
INSERT INTO org_tweet (Tweet_Text, block) values ('hell there', 0);
INSERT INTO org_tweet (Tweet_Text, block) values ('hellooo there', 0);
INSERT INTO org_tweet (Tweet_Text, block) values ('hello here', 0);
INSERT INTO org_tweet (Tweet_Text, block) values ('hey there', 0);

INSERT INTO AllTweets (Tweet_ID, User_ID, Date_Time, Retweet_bit) values (1, 1, '1/2/2014', 0);
INSERT INTO AllTweets (Tweet_ID, User_ID, Date_Time, Retweet_bit) values (2, 1, '1/2/2014', 0);
INSERT INTO AllTweets (Tweet_ID, User_ID, Date_Time, Retweet_bit) values (3, 2, '1/2/2014', 0);
INSERT INTO AllTweets (Tweet_ID, User_ID, Date_Time, Retweet_bit) values (4, 4, '1/2/2014', 0);
INSERT INTO AllTweets (Tweet_ID, User_ID, Date_Time, Retweet_bit) values (1, 2, '1/2/2014', 1);
INSERT INTO AllTweets (Tweet_ID, User_ID, Date_Time, Retweet_bit) values (1, 3, '1/2/2014', 1);
INSERT INTO AllTweets (Tweet_ID, User_ID, Date_Time, Retweet_bit) values (2, 2, '1/2/2014', 1);



INSERT INTO Followers values (1, 2);
INSERT INTO Followers values (2, 1);
INSERT INTO Followers values (3, 2);
INSERT INTO Followers values (2, 3);
INSERT INTO Followers values (1, 3);
INSERT INTO Followers values (3, 1);



///////// VIEWS ////////

--1.
--view for getting followers count for each user for admin
create view eachfollowerscount
AS
    Select F.User_ID,count(*) AS NoOfFollowers
	FROM Followers F JOIN UoT U on U.User_ID=F.f_ids
	group by F.User_ID

--2.
-- view to get names of all followers of all users

CREATE VIEW FollowersName

AS
Select F.User_ID AS UserID ,U.User_Name AS FollowersName
From Followers F inner join UoT U on F.f_ids=U.User_ID 


--3.

-- view for getting names for all senders and receivers

CREATE VIEW SenderReceiverName

AS
Select U.User_Name AS SENDERNAME,U2.User_Name AS RECEIVERNAME , M.Message AS Message
From message M inner join UoT U on M.Sender_Id=U.User_ID 
	 inner join UoT U2 on M.Reciever_Id=U2.User_ID 
--4.
-- view for getting names for all senders
CREATE VIEW SenderName

AS
Select U.User_Name AS SENDERNAME,M.Reciever_Id as ReceiverID ,M.Message AS Message
From message M inner join UoT U on M.Sender_Id=U.User_ID 
	


--5.
-- view for getting names for all receivers

CREATE VIEW ReceiverName

AS
Select m.Sender_Id AS SenderID,u2.User_Name as ReceiverName, M.message AS Message
From message M 
	 inner join UoT U2 on M.Reciever_Id=U2.User_ID 
