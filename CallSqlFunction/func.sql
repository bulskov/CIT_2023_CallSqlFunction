CREATE OR REPLACE FUNCTION "public"."insertcategory"("id" int4, "name" varchar, "description" varchar)
  RETURNS "pg_catalog"."void" AS $BODY$
            BEGIN
               insert into categories values(id, name, description);
            END
            $BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100



CREATE OR REPLACE FUNCTION "public"."search"("pattern" varchar)
  RETURNS TABLE("p_id" int4, "p_name" varchar) AS $BODY$
BEGIN
   RETURN QUERY 
	 SELECT productid, productname
   FROM products
   WHERE productname LIKE pattern;
END; $BODY$
  LANGUAGE plpgsql VOLATILE
  COST 100
  ROWS 1000


