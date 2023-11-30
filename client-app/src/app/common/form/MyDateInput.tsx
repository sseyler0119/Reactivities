import { useField } from 'formik';
import { Form, Label } from 'semantic-ui-react';
import DatePicker, {ReactDatePickerProps} from 'react-datepicker';

//Partial makes the properties optional, w/out this onChange throws an error because it is not passed down
const MyDateInput = (props: Partial<ReactDatePickerProps>) => { 
  const [field, meta, helpers] = useField(props.name!); // ! bang property says we know that the name property will be there
  return (
    //  double !! makes the object into a boolean, meta.error is a string,
    // so we are casting it into a boolean based on whether it is undefined
    <Form.Field error={meta.touched && !!meta.error}>
       <DatePicker 
            {...field}
            {...props}
            selected={(field.value && new Date(field.value)) || null}
            onChange={value => helpers.setValue(value)}
       />
        
      {meta.touched && meta.error ? (
        <Label basic color='red'>
          {meta.error}
        </Label>
      ) : null}
    </Form.Field>
  );
};
export default MyDateInput;
