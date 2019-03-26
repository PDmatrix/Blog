import { Form, FormikProps, FormikValues, withFormik } from "formik";
import React from "react";
import * as Yup from "yup";
import { PostsApi, PreviewDto, PreviewRequest } from "../../api";
import FieldWrapper from "../common/form/FieldWrapper";

const PreviewPostSchema = Yup.object().shape({
  content: Yup.string().required("Required field")
});

const PreviewPostForm = ({
  isSubmitting,
  status
}: FormikProps<PreviewRequest>) => {
  return (
    <>
      <Form>
        <FieldWrapper type="textarea" label="Content" name="content" />
        <button type="submit" disabled={isSubmitting}>
          Preview post
        </button>
      </Form>
      {status && <div dangerouslySetInnerHTML={{ __html: status }} />}
    </>
  );
};

const PreviewPostFormik = withFormik<PreviewRequest, FormikValues>({
  handleSubmit: async (
    values: PreviewRequest,
    { setSubmitting, setStatus }
  ) => {
    const api = new PostsApi();
    const preview: PreviewDto = (await api.postsPreview(values)).data;
    setStatus(preview.content);
    setSubmitting(false);
  },
  mapPropsToValues: (props: PreviewRequest) => ({
    content: props.content || ""
  }),
  validationSchema: PreviewPostSchema
})(PreviewPostForm);

export default PreviewPostFormik;
