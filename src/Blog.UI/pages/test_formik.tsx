import {
  ErrorMessage,
  Field,
  FieldProps,
  Form,
  Formik,
  FormikValues
} from "formik";
import React from "react";
import * as Yup from "yup";

const SignupSchema = Yup.object().shape({
  email: Yup.string()
    .email("Invalid email")
    .required("Required"),
  firstName: Yup.string()
    .min(2, "Too Short!")
    .max(50, "Too Long!")
    .required("Required"),
  lastName: Yup.string()
    .min(2, "Too Short!")
    .max(50, "Too Long!")
    .required("Required")
});

const OnSubmit = (values: FormikValues) => {
  alert(JSON.stringify(values, null, 2));
};

const ValidationSchemaExample = () => (
  <div>
    <h1>Signup</h1>
    <Formik
      initialValues={{
        email: "",
        firstName: "",
        lastName: ""
      }}
      validationSchema={SignupSchema}
      onSubmit={OnSubmit}
    >
      {() => (
        <Form>
          <Field name="firstName">
            {({ field }: FieldProps) => (
              <div>
                <label htmlFor="firstName">First Name</label>
                <input {...field} id="firstName" />
              </div>
            )}
          </Field>
          <ErrorMessage name="firstName" />
          <Field name="lastName">
            {({ field }: FieldProps) => (
              <div>
                <label>Last Name</label>
                <input {...field} />
              </div>
            )}
          </Field>
          <ErrorMessage name="lastName" />
          <Field name="email">
            {({ field }: FieldProps) => (
              <div>
                <label>Email</label>
                <input {...field} type="email" />
              </div>
            )}
          </Field>
          <ErrorMessage name="email" />
          <button type="submit">Submit</button>
        </Form>
      )}
    </Formik>
  </div>
);

export default ValidationSchemaExample;
