drop table contactlog

CREATE TABLE contactLog (
    ID int identity not null,
	salutation varchar(10) null,
    Name varchar(255) null,
    Email varchar(255) null,
    [subject] varchar(255) null,
    message varchar(255) null,
	LastUpdated datetime2 not null
);

select * from contactlog