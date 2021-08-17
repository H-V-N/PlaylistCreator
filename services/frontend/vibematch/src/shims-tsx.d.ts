import Vue, { VNode } from 'vue';

declare global {
  namespace JSX {
    // tslint:disable no-empty-interface
    type Element = VNode;
    // tslint:disable no-empty-interface
    type ElementClass = Vue;
    interface IntrinsicElements {
      [elem: string]: any;
    }
  }

  type SpotifyCallback<ExpiryType> = {
    access_token: string;
    token_type: 'Bearer';
    expires_in: ExpiryType;
    state?: string;
  };
}
