﻿var CurrentPage = function () {

    var handleLogin = function () {

        var $loginForm = $('.login-form');

        $loginForm.validate({
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                }
            }
        });

        $loginForm.submit(function (e) {
            e.preventDefault();
            console.log("111");
            if (!$('.login-form').valid()) {
                return;
            }

            abp.ui.setBusy(
                null,
                abp.ajax({
                    contentType: "application/x-www-form-urlencoded",
                    url: $loginForm.attr('action'),
                    data: $loginForm.serialize()
                })
            );
        });

        $('a.social-login-icon').click(function () {
            var $a = $(this);
            var $form = $a.closest('form');
            $form.find('input[name=provider]').val($a.attr('data-provider'));
            $form.submit();
        });

        $loginForm.find('input[name=returnUrlHash]').val(location.hash);

        $('input[type=text]').first().focus();
    }

    return {
        init: function () {
            handleLogin();
        }
    };

}();