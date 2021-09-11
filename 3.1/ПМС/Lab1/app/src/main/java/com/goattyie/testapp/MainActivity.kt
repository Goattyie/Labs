package com.goattyie.testapp

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.view.View

class MainActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
    }

    fun click_btn(view: View)
    {
        val intent = Intent(this, SecondActivity::class.java)
        intent.putExtra("second_name", "Саевский")
        startActivity(intent)
    }
}