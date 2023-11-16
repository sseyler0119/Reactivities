import { useField } from 'formik';
import { Form, Label } from 'semantic-ui-react';

interface Props {
  placeholder: string;
  name: string;
  rows: number;
  label?: string;
}

const MyTextArea = (props: Props) => {
  const [field, meta] = useField(props.name);
  return (
    //  double !! makes the object into a boolean, meta.error is a string,
    // so we are casting it into a boolean based on whether it is undefined
    <Form.Field error={meta.touched && !!meta.error}>
      <label>{props.label}</label>
      <textarea {...field} {...props} />
      {meta.touched && meta.error ? (
        <Label basic color='red'>
          {meta.error}
        </Label>
      ) : null}
    </Form.Field>
  );
};
export default MyTextArea;
