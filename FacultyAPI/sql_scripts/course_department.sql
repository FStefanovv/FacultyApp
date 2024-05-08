CREATE OR REPLACE FUNCTION check_course_department() RETURNS TRIGGER AS $$
DECLARE
    teachers_department "Teachers"."Department"%TYPE;
BEGIN
    SELECT "Department" INTO teachers_department
    FROM "Teachers" WHERE "Id"=NEW."TeacherId";
    IF NEW."Department" != teachers_department THEN
        RAISE EXCEPTION 'Course must belong to the same department as the teacher teaching it';
    ELSE RETURN NEW;
    END IF;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER trg_course_department
BEFORE INSERT ON "Courses"
FOR EACH ROW
EXECUTE FUNCTION check_course_department();