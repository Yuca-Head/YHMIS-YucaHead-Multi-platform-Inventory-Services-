INSERT INTO "Identity"."AspNetRoles"
    ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES
    (1, 'Admin', 'ADMIN', gen_random_uuid()::text),
    (2, 'Employee', 'EMPLOYEE', gen_random_uuid()::text);