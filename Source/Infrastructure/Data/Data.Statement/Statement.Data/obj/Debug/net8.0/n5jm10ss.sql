CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'statement') THEN
        CREATE SCHEMA statement;
    END IF;
END $EF$;

CREATE TABLE statement.accounts (
    id uuid NOT NULL,
    agency character varying(100) NOT NULL,
    accountnumber text NOT NULL,
    dac integer NOT NULL,
    accountname text NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone NOT NULL,
    isdraft boolean NOT NULL,
    data JSONB,
    CONSTRAINT "PK_accounts" PRIMARY KEY (id)
);

CREATE TABLE statement.cash_composition (
    id uuid NOT NULL,
    productname character varying(100) NOT NULL,
    productcode integer NOT NULL,
    investedbalance numeric NOT NULL,
    CONSTRAINT "PK_cash_composition" PRIMARY KEY (id)
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250308191843_NomeDaMigracao', '9.0.2');

COMMIT;

