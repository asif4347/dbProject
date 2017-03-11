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
