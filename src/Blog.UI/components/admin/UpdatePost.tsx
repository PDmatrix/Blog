import { Form, FormikProps, FormikValues, withFormik } from "formik";
import React from "react";
import * as Yup from "yup";
import { PostRequest } from "../../api";
import FieldWrapper from "../common/form/FieldWrapper";

const UpdatePostSchema = Yup.object().shape({
  content: Yup.string().required("Required field"),
  excerpt: Yup.string().required("Required field"),
  title: Yup.string().required("Required field")
});

const UpdatePostForm = ({ isSubmitting }: FormikProps<PostRequest>) => {
  return (
    <Form>
      <FieldWrapper type="text" label="Content" name="content" />
      <FieldWrapper type="text" label="Excerpt" name="excerpt" />
      <FieldWrapper type="text" label="Title" name="title" />
      <button type="submit" disabled={isSubmitting}>
        Update post
      </button>
    </Form>
  );
};

const UpdatePostFormik = withFormik<PostRequest, FormikValues>({
  handleSubmit: (values: PostRequest, { setSubmitting }) => {
    setTimeout(() => {
      alert(JSON.stringify(values, null, 2));
      setSubmitting(false);
    }, 1000);
  },
  mapPropsToValues: (props: PostRequest) => ({
    content: props.content || "",
    title: props.title || "",
    excerpt: props.excerpt || ""
  }),
  validationSchema: UpdatePostSchema
})(UpdatePostForm);

export default UpdatePostFormik;
