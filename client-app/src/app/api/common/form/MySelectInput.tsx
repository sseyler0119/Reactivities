import { useField } from 'formik';
import { Form, Label, Select } from 'semantic-ui-react';

interface Props {
  placeholder: string;
  name: string;
  options: {text: string, value: string}[];
  label?: string;
}

const MySelectInput = (props: Props) => {
  const [field, meta, helpers] = useField(props.name);
  return (
    //  double !! makes the object into a boolean, meta.error is a string,
    // so we are casting it into a boolean based on whether it is undefined
    <Form.Field error={meta.touched && !!meta.error}>
      <label>{props.label}</label>
      <Select
        clearable
        options={props.options}
        value={field.value || null}
        /* use _ for first param instead of event to tell typescript we are not going to use the parameter 
            using (e, data) will cause the linter to show an error since the variable is unused*/
        onChange={(_, data) => helpers.setValue(data.value)}
        onBlur={() => helpers.setTouched(true)}
        placeholder={props.placeholder}
      />
      {meta.touched && meta.error ? (
        <Label basic color='red'>
          {meta.error}
        </Label>
      ) : null}
    </Form.Field>
  );
};
export default MySelectInput;
