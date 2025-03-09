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

CREATE TABLE statement.investments (
    id uuid NOT NULL,
    productid uuid NOT NULL,
    accountid uuid NOT NULL,
    investedbalance numeric(18,2) NOT NULL,
    investedat timestamp with time zone NOT NULL,
    CONSTRAINT "PK_investments" PRIMARY KEY (id),
    CONSTRAINT "FK_investments_accounts_accountid" FOREIGN KEY (accountid) REFERENCES statement.accounts (id) ON DELETE CASCADE,
    CONSTRAINT "FK_investments_cash_composition_productid" FOREIGN KEY (productid) REFERENCES statement.cash_composition (id) ON DELETE CASCADE
);

CREATE INDEX "IX_investments_accountid" ON statement.investments (accountid);

CREATE INDEX "IX_investments_productid" ON statement.investments (productid);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250308194958_NomeDaMigracao', '9.0.2');

COMMIT;

