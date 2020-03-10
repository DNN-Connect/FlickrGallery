import * as React from "react";
import * as Models from "../Models/";

interface IRefreshButtonProps {
  module: Models.IAppModule;
}

interface IRefreshButtonState {
  refreshing: boolean;
}

export default class RefreshButton extends React.Component<
  IRefreshButtonProps,
  IRefreshButtonState
> {
  constructor(props: IRefreshButtonProps) {
    super(props);
    this.state = {
      refreshing: false
    };
  }

  refresh(e: React.MouseEvent<HTMLAnchorElement, MouseEvent>): void {
    e.preventDefault();
    if (this.state.refreshing) { return; }
    if (confirm(this.props.module.resources.RefreshConfirm)) {
      this.setState({
        refreshing: true
      });
      this.props.module.service.refresh(() => {
        this.setState({
          refreshing: false
        });
      });
    }
  }

  public render(): JSX.Element {
    var btn = null;
    if (this.props.module.security.IsAdmin) {
      if (this.state.refreshing) {
        btn = <span className="dnnSecondaryAction disabled">{this.props.module.resources.Refreshing}</span>
      } else {
        btn = <a href="#" className="dnnSecondaryAction" onClick={e => this.refresh(e)}>{this.props.module.resources.Refresh}</a>;
      }
    }
    return btn;
  }
}
