create table Users(
	userID int primary key not null,
	userName varchar(50) not null,
	emailAddress varchar(100),
	pass varchar(50) not null,
	conatctNo int,
	DP varchar(100)
)
create table Admins(
	adminID int primary key not null foreign key references Users(userID) on delete no action on update no action,
	adminName varchar(50) not null
)
create table PublicFigure(
	publicFigureID int foreign key references Users(userID) on delete no action on update no action,
	publicFigureName varchar(50)
)
create table Followers(
	followerID int primary key foreign key references Users(userID) on delete no action on update no action,
	followerSID int not null foreign key references Users(userID) on delete no action on update no action
)
create table Tweets(
	tweetID int primary key,
	userID int not null foreign key references Users(userID) on delete no action on update no action,
	tweetText varchar(250)
)
create table Trends(
	trendID int primary key,
	trendText varchar(50) not null
)
create table Twitter(
	userID int not null foreign key references Users(userId) on delete no action on update no action,
	tweetId int not null foreign key references Tweets(tweetId) on delete no action on update no action,
	tweetTime Timestamp not null,
	tweetDate Date not null,
	trendId int foreign key references Trends(trendId) on delete no action on update no action,
)
create table Msgs(
	msgID int primary key,
	msgText varchar(50),
	msgtime time not null,
	senderID int not null foreign key references Users(userId) on delete no action on update no action,
	receiverID int not null foreign key references Users(userId) on delete no action on update no action
)