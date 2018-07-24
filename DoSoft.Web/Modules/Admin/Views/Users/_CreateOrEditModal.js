(function ($) {
    app.modals.CreateOrEditUserModal = function () {

        var _userService = abp.services.app.userAppServicecs;

        var _modalManager;
        var _$userInformationForm = null;


        this.init = function (modalManager) {
            _modalManager = modalManager;

            _$userInformationForm = _modalManager.getModal().find('form[name=UserInformationsForm]');
            _$userInformationForm.validate();

            var passwordInputs = _modalManager.getModal().find('input[name=Password],input[name=PasswordRepeat]');
            var passwordInputGroups = passwordInputs.closest('.form-group');

            _modalManager.getModal().find('[data-toggle=tooltip]').tooltip();
        };

        this.save = function () {
            if (!_$userInformationForm.valid()) {
                return;
            }
            var user = _$userInformationForm.serializeFormToObject();

            _modalManager.setBusy(true);
            _userService.createOrUpdateUser({
                user: user,
                sendActivationEmail: user.SendActivationEmail
             }).done(function () {
                abp.notify.info("保存成功");
                _modalManager.close();
                abp.event.trigger('app.createOrEditUserModalSaved');
            }).always(function () {
                _modalManager.setBusy(false);
            });
        };
    };
})(jQuery);