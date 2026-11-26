SELECT
    current_database() AS "database",
    current_user AS usuario,
    inet_server_addr() AS servidor,
    version() AS "version";

SELECT * FROM "Identity"."AspNetRoles"