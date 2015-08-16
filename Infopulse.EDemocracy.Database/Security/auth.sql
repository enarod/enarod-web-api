CREATE SCHEMA [auth]
    AUTHORIZATION [dbo];


GO
GRANT VIEW CHANGE TRACKING
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT VIEW DEFINITION
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT UPDATE
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT TAKE OWNERSHIP
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT SELECT
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT REFERENCES
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT INSERT
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT EXECUTE
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT DELETE
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT CREATE SEQUENCE
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT CONTROL
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT ALTER
    ON SCHEMA::[auth] TO [enarod_app_admin];


GO
GRANT UPDATE
    ON SCHEMA::[auth] TO [enarod_app_dev];


GO
GRANT SELECT
    ON SCHEMA::[auth] TO [enarod_app_dev];


GO
GRANT REFERENCES
    ON SCHEMA::[auth] TO [enarod_app_dev];


GO
GRANT INSERT
    ON SCHEMA::[auth] TO [enarod_app_dev];


GO
GRANT EXECUTE
    ON SCHEMA::[auth] TO [enarod_app_dev];


GO
GRANT DELETE
    ON SCHEMA::[auth] TO [enarod_app_dev];

