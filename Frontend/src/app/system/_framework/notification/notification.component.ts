import { Component, OnInit, OnDestroy, Input, ViewEncapsulation, NgZone } from '@angular/core';
import { Notification } from './models/notification.type';
import { NotificationsService } from './notifications.service';
@Component({
    selector: 'notification',
    encapsulation: ViewEncapsulation.None,
    templateUrl: './notification.html',
    styleUrls: ['./notification.css']
})

export class NotificationComponent implements OnInit, OnDestroy {

    @Input() public timeOut: number;
    @Input() public showProgressBar: boolean;
    @Input() public pauseOnHover: boolean;
    @Input() public clickToClose: boolean;
    @Input() public maxLength: number;
    @Input() public theClass: string;
    @Input() public rtl: boolean;
    @Input() public animate: string;
    @Input() public position: number;
    @Input() public item: Notification;


    // Progress bar variables
    public progressWidth: number = 0;
    private stopTime: boolean = false;
    private timer: any;
    private steps: number;
    private speed: number;
    private count: number = 0;
    private start: any;
    private diff: any;


    constructor(
        private notificationService: NotificationsService,
        private zone: NgZone
    ) { }

    ngOnInit(): void {
        if (this.animate) {
            this.item.state = this.animate;
        }
        if (this.item.override) {
            this.attachOverrides();
        }
        if (this.timeOut !== 0) {
            this.startTimeOut();
        }
    }

    startTimeOut(): void {
        this.steps = this.timeOut / 10;
        this.speed = this.timeOut / this.steps;
        this.start = new Date().getTime();
        this.zone.runOutsideAngular(() => this.timer = setTimeout(this.instance, this.speed));
    }

    onEnter(): void {
        if (this.pauseOnHover) {
            this.stopTime = true;
        }
    }

    onLeave(): void {
        if (this.pauseOnHover) {
            this.stopTime = false;
            setTimeout(this.instance, (this.speed - this.diff));
        }
    }

    setPosition(): number {
        return this.position !== 0 ? this.position * 90 : 0;
    }

    onClick($e: MouseEvent): void {
        this.item.click!.emit($e);

        if (this.clickToClose) {
            this.remove();
        }
    }

    // Attach all the overrides
    attachOverrides(): void {
        Object.keys(this.item.override).forEach(a => {
            if (this.hasOwnProperty(a)) {
                (<any>this)[a] = this.item.override[a];
            }
        });
    }

    ngOnDestroy(): void {
        clearTimeout(this.timer);
    }

    private instance = () => {
        this.zone.runOutsideAngular(() => {
            this.zone.run(() => this.diff = (new Date().getTime() - this.start) - (this.count * this.speed));

            if (this.count++ === this.steps) this.zone.run(() => this.remove());
            else if (!this.stopTime) {
                if (this.showProgressBar) this.zone.run(() => this.progressWidth += 100 / this.steps);

                this.timer = setTimeout(this.instance, (this.speed - this.diff));
            }
        })
    };

    private remove() {
        if (this.animate) {
            this.item.state = this.animate + 'Out';
            this.zone.runOutsideAngular(() => {
                setTimeout(() => {
                    this.zone.run(() => this.notificationService.set(this.item, false))
                }, 310);
            })
        } else {
            this.notificationService.set(this.item, false);
        }
    }
}
