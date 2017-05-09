drop table contactlog

CREATE TABLE contactLog (
    [ID] int identity not null,
    [Name] varchar(255) not null,
    [Email] varchar(255) not null,
    [Message] varchar(255) not null,
	[LastUpdated] datetime2 not null
);
