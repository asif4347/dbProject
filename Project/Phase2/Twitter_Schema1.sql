create table dbo.UoT
(	User_ID int primary key identity (1,1),
	User_Name varchar (20),
	DP varchar (50),
	DoB date,
	Phone_no varchar (50),
	Email varchar (50),
	Password varchar (50),
	block int null
)
create table Admin(
User_ID int primary key not null foreign key references UoT(User_ID) on delete  cascade on update cascade,
Admin_Name varchar (20)
)
create table Followers(
User_ID int  foreign key references UoT(User_ID) on delete no action on update no action,
f_ids int  foreign key references UoT(User_ID) on delete no action on update no action,
primary key (User_ID, f_ids)
)
create table Trend(
Trend_ID int  primary key not null identity(1,1),
trend_text varchar (21)
)

create table org_tweet(
Tweet_ID int primary key identity(1,1),
Tweet_text varchar(270),
block bit 
)

create table AllTweets(
Tweet_ID int foreign key references org_tweet(Tweet_ID),  
User_ID int foreign key references UoT(User_ID) on delete no action on update no action,
Date_Time Datetime,
Retweet_bit bit,
primary key (Tweet_ID, User_ID)

)

create table tweet_trend
( Tweet_ID int foreign key references org_tweet(Tweet_ID),
  Trend_ID int foreign key references Trend(Trend_ID),
  primary key (Tweet_ID, Trend_ID)
)
create table message(
msg_Id int identity(1,1) primary key,
Sender_Id int not null foreign key references UoT(User_ID),
Reciever_Id int not null foreign key references UoT(User_ID),
Message varchar(500),
[Date and Time] datetimeoffset
)