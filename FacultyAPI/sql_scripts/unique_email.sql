CREATE OR REPLACE FUNCTION check_email_unique() RETURNS TRIGGER AS $$
BEGIN
    IF  EXISTS (SELECT 1 FROM "Students" WHERE "Email" = NEW."Email") OR 
        EXISTS (SELECT 1 FROM "Teachers" WHERE "Email" = NEW."Email") THEN 
        RAISE EXCEPTION 'Email already in use';
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_check_email_unique_students
BEFORE INSERT ON "Students"
FOR EACH ROW
EXECUTE FUNCTION check_email_unique();

CREATE TRIGGER trg_check_email_unique_teachers
BEFORE INSERT ON "Teachers"
FOR EACH ROW
EXECUTE FUNCTION check_email_unique();
