import { Injectable, EventEmitter } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { NotificationEvent } from './models/notification-event.type';
import { Notification } from './models/notification.type';
import { Icons, defaultIcons } from './models/icons';

@Injectable()
export class NotificationsService {

  private emitter: Subject<NotificationEvent> = new Subject<NotificationEvent>();
  private icons: Icons = defaultIcons;

  set(notification: Notification, to: boolean) {
    notification.id = notification.override && notification.override.id ? notification.override.id : Math.random().toString(36).substring(3);
    notification.click = new EventEmitter<{}>();
    this.emitter.next({ command: 'set', notification: notification, add: to });
    return notification;
  };

  getChangeEmitter() {
    return this.emitter;
  }

  //// Access methods
  success(content: string, override?: any) {
    return this.set({
      content: content,
      type: 'success',
      icon: this.icons.success,
      override: override
    }, true);
  }

  error(content: string, override?: any) {
    return this.set({ content: content, type: 'error', icon: this.icons.error, override: override }, true);
  }

  alert(content: string, override?: any) {
    return this.set({ content: content, type: 'alert', icon: this.icons.alert, override: override }, true);
  }

  info(content: string, override?: any) {
    return this.set({ content: content, type: 'info', icon: this.icons.info, override: override }, true);
  }

  bare(content: string, override?: any) {
    return this.set({ content: content, type: 'bare', icon: 'bare', override: override }, true);
  }

  // With type method
  create(content: string, type: string, override?: any) {
    return this.set({ content: content, type: type, icon: 'bare', override: override }, true);
  }

  // HTML Notification method
  html(html: any, type: string, override?: any) {
    return this.set({ html: html, type: type, icon: 'bare', override: override }, true);
  }

  // Remove all notifications method
  remove(id?: string) {
    if (id) this.emitter.next({ command: 'clean', id: id });
    else this.emitter.next({ command: 'cleanAll' });
  }

}
