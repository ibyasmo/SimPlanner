﻿import angular from 'angular';

    'use strict';
    var controllerId = 'calendar';
export default angular.module('app').controller(controllerId, ['common', '$q', 'breeze', 'datacontext','$scope', '$location',calendar]).name;

    function calendar(common, $q, breeze, datacontext,$scope, $location) {
        var vm = this;
        var log = common.logger.getLogFn(controllerId);
        var _retrieved = new Set();  

        vm.calendarView = 'month';
        vm.events = [];
        vm.eventClicked = eventClicked;
        vm.isCellOpen = true;
        vm.viewDate = new Date();

        activate();

        $scope.$watch(function () { return vm.viewDate; }, viewDateChanged);
        $scope.$watch(function () { return vm.calendarView; }, viewDateChanged);


        function activate() {
            common.activateController(
                [retrieveAppointments(vm.viewDate.getFullYear(), vm.viewDate.getMonth())], controllerId) //notification of login after dashboard activated
                .then(function () {
                    log('Activated calendar View');
                });
        }

        function eventClicked(calendarEvent) {
            $location.path('/course/' + calendarEvent.id);
        }

        function viewDateChanged() {
            if (vm.calendarView === 'year') {
                retrieveAppointments(vm.viewDate.getFullYear());
            } else {
                retrieveAppointments(vm.viewDate.getFullYear(), vm.viewDate.getMonth());
            }
        }
		
        function retrieveAppointments(year:number, month?: number) {
            year *= 12;
            var getMonthYear = function (monthYear) {
                var yr = Math.floor(monthYear / 12);
                return {
                    year: yr,
                    month: monthYear - yr * 12
                };
            };
            var startMonth = month === void 0 ? year : year + month - 1;
            var finishMonth = month === void 0 ? year + 11 : year + month + 1;
            var dtRanges = createDateRanges();
            for (; startMonth <= finishMonth; startMonth++) {
                if (!_retrieved.has(startMonth)) {
                    dtRanges.includeMonth(getMonthYear(startMonth));
                    _retrieved.add(startMonth);
                }
            }

			if (!dtRanges.length){
				return $q.when();
			}
			
			var predicate = dtRanges.map(function (el) {
                return new breeze.Predicate.create('startFacultyUtc', '>=', el.start).and('startFacultyUtc','<=',el.finish);
			}).reduce(function(a,b){ 
				return a.or(b); 
			});
			
			var returnedCourses;
			return $q.all([datacontext.ready(),
                datacontext.courses.find({
                    where: predicate,
                    expand: 'courseDays'
                }).then(function (data) {
                    returnedCourses = data;
                })]).then(function () {
                    vm.events = returnedCourses.map(mapCourse).reduce(function (a, b) {
                        return a.concat(b);
                    }, vm.events);
                });
		}
    }

    //must have months included sequentially for function to work - i.e. not for broader use without redesign
    function createDateRanges() {
        var returnVar:any = [];
        returnVar.includeMonth = includeMonth;

        return returnVar;

        function includeMonth(argObj /*{year, month}*/) {
            if (returnVar.length) {
                var last = returnVar[returnVar.length - 1];
                if (last.finish.getMonth() === argObj.month) {
                    last.finish = new Date(argObj.year, argObj.month + 1, 1);
                    return;
                }
            }
            returnVar.push({
                start: new Date(argObj.year, argObj.month, 1),
                finish: new Date(argObj.year, argObj.month + 1, 1)
            });
        }
    }


    function mapCourse(course) {
        var day1:any = {
            id: course.id,
            title: '<i class="glyphicon glyphicon-asterisk"></i> <span class="text-primary">' + course.courseFormat.courseType.abbreviation + '</span> <small>' + course.courseFormat.description + '</small>',
            color: { 
                primary: course.department.primaryColourHtml, // the primary event color (should be darker than secondary)
                secondary: course.department.secondaryColourHtml // the secondary event color (should be lighter than primary)
            },
            //safeClassName(course.department.abbreviation),
            startsAt: course.startFacultyUtc,
            endsAt: course.finish,
            draggable: true,
            resizable: true
        };
        if (course.cancelled) {
            day1.cssClass = 'cancelled';
            delete day1.color;
        }
        return [day1].concat(course.courseDays.map(function (el) {
            var clone = angular.copy(day1);
            clone.startsAt = el.startFacultyUtc;
            clone.endsAt = el.finish;
            clone.title += ' day '+ el.day;
            return clone;
        }));
    }

    /*
    function safeClassName(dptAbbrev) {
        return dptAbbrev.replace(/[^\w\d]/g, '').toLowerCase();
    }

    function createClasses(departmentList) {
        var cssId = 'departmentColourCSS';
        var style = document.getElementById(cssId);
        if (style) { return; }
        var styleDefs = [];
        style = document.createElement('style');
        style.id = cssId;
        style.type = 'text/css';
        //style.media = 
        style.innerHTML = departmentList.map(function (dpt) {
            var className = safeClassName(dpt.abbreviation);
            return '.' + className + ' { background-color: #' + dpt.colour + ' }';
        }).join('\n');

        document.getElementsByTagName('head')[0].appendChild(style);
    }
    */