create extension if not exists "uuid-ossp";

create table "Users" (
    "Id" uuid not null primary key default uuid_generate_v4(),
    "CreatedAt" timestamp not null default (now() at time zone 'utc'),
    "UpdatedAt" timestamp not null default (now() at time zone 'utc'),
    "IsActive" boolean not null default true,
    "Username" varchar(96) not null unique,
    "Password" varchar(64) not null,
    "Role" int not null
);

create table "Customers" (
    "Id" uuid not null primary key default uuid_generate_v4(),
    "CreatedAt" timestamp not null default (now() at time zone 'utc'),
    "UpdatedAt" timestamp not null default (now() at time zone 'utc'),
    "IsActive" boolean not null default true,
    "CreatedById" uuid not null references "Users" ("Id"),
    "UpdatedById" uuid not null references "Users" ("Id"),
    "EmailAddress" varchar(96) not null unique,
    "Name" varchar(256) not null,
    "Gender" int not null
);

create table "Products" (
    "Id" uuid not null primary key default uuid_generate_v4(),
    "CreatedAt" timestamp not null default (now() at time zone 'utc'),
    "UpdatedAt" timestamp not null default (now() at time zone 'utc'),
    "IsActive" boolean not null default true,
    "CreatedById" uuid not null references "Users" ("Id"),
    "UpdatedById" uuid not null references "Users" ("Id"),
    "Name" varchar(128) not null,
    "CostPrice" decimal not null,
    "Price" decimal not null
);

create table "Orders" (
    "Id" uuid not null primary key default uuid_generate_v4(),
    "CreatedAt" timestamp not null default (now() at time zone 'utc'),
    "UpdatedAt" timestamp not null default (now() at time zone 'utc'),
    "IsActive" boolean not null default true,
    "CreatedById" uuid not null references "Users" ("Id"),
    "UpdatedById" uuid not null references "Users" ("Id"),
    "OrderStatus" int not null,
    "OrderNumber" serial not null,
    "CustomerId" uuid not null references "Customers" ("Id")
);

create table "OrderLines" (
    "Id" uuid not null primary key default uuid_generate_v4(),
    "CreatedAt" timestamp not null default (now() at time zone 'utc'),
    "UpdatedAt" timestamp not null default (now() at time zone 'utc'),
    "IsActive" boolean not null default true,
    "CreatedById" uuid not null references "Users" ("Id"),
    "UpdatedById" uuid not null references "Users" ("Id"),
    "Quantity" int not null,
    "ProductId" uuid not null references "Products" ("Id"),
    "OrderId" uuid not null references "Orders" ("Id")
);

do $$
declare
    the_password varchar(64) := '8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918'; -- admin
begin
    insert into "Users" ("Username", "Password", "Role") values ('admin@microcrm.com', the_password, 0);
end $$;
