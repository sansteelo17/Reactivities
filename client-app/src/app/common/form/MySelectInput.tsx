import { useField } from "formik";
import { Form, Label, Select } from "semantic-ui-react";

type Option = { text: string; value: string };

interface Props {
  placeholder: string;
  name: string;
  options: Option[];
  label?: string;
}

const MySelectInput = (props: Props) => {
  const [field, meta, helpers] = useField(props.name);

  return (
    <Form.Field error={meta.touched && !!meta.error}>
      {props.label && <label>{props.label}</label>}
      <Select
        clearable
        options={props.options}
        value={field.value || null}
        onChange={(_, data) => helpers.setValue(data.value)}
        onBlur={() => helpers.setTouched(true)}
        placeholder={props.placeholder}
      />
      {meta.touched && meta.error ? (
        <Label basic color="red">
          {meta.error}
        </Label>
      ) : null}
    </Form.Field>
  );
};

export default MySelectInput;
