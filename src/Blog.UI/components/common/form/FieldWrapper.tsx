import { ErrorMessage, Field } from "formik";
import React, { HTMLProps } from "react";

interface IFieldWrapper {
  label: string;
  name: string;
  type: string;
  children?: any;
}

const FieldWrapper = ({
  children,
  label,
  name,
  type,
  ...otherProps
}: IFieldWrapper & HTMLProps<any>) => {
  if (children) {
    return children;
  }

  return (
    <div>
      <label>
        {label}
        <Field type={type} name={name} {...otherProps} />
      </label>
      <ErrorMessage name={name} />
    </div>
  );
};

export default FieldWrapper;
