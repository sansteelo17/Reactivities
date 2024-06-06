import { FC } from "react";
import { Dimmer, Loader } from "semantic-ui-react";

interface ILoadingComponent {
  inverted?: boolean;
  content?: string;
}

const LoadingComponent: FC<ILoadingComponent> = ({
  inverted = true,
  content = "Loading...",
}) => {
  return (
    <Dimmer active={true} inverted={inverted}>
      <Loader content={content} />
    </Dimmer>
  );
};

export default LoadingComponent;
