CREATE TABLE post
(
	id         serial NOT NULL
		CONSTRAINT post_pk
			PRIMARY KEY,
	content    text   NOT NULL,
	created_at timestamp WITH TIME ZONE DEFAULT now(),
	updated_at timestamp WITH TIME ZONE DEFAULT now()
);

COMMENT ON TABLE post IS 'Posts';

ALTER TABLE post
	OWNER TO postgres;

CREATE UNIQUE INDEX post_id_uindex
	ON post (id);

CREATE FUNCTION update_modified_column() RETURNS trigger
	LANGUAGE plpgsql
AS
$$
BEGIN
	IF ROW (NEW.*) IS DISTINCT FROM ROW (OLD.*) THEN
		NEW.updated_at = now();
		RETURN NEW;
	ELSE
		RETURN OLD;
	END IF;
END;
$$;

ALTER FUNCTION update_modified_column() OWNER TO postgres;

CREATE TRIGGER update_post_modtime
	BEFORE UPDATE
	ON post
	FOR EACH ROW
EXECUTE PROCEDURE update_modified_column();
