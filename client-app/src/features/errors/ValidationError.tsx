import { FC } from "react";
import { Message } from "semantic-ui-react";

interface IValidationErrors {
  errors: string[];
}

const ValidationError: FC<IValidationErrors> = ({ errors }) => {
  return (
    <Message error>
      {errors && (
        <Message.List>
          {errors.map((err: string, index) => (
            <Message.Item key={index}>{err} </Message.Item>
          ))}
        </Message.List>
      )}
    </Message>
  );
};

export default ValidationError;
